using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;

namespace InventoryEndpoint.Handlers
{
    public class InventoryHandler : Saga<ShipInventoryData>, IHandleMessages<OrderPlaced>, IAmStartedByMessages<InitializeShipInventory>
    {
        private static readonly ILog log = LogManager.GetLogger<InventoryHandler>();
        private readonly IBus bus;

        public InventoryHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(OrderPlaced message)
        {
            log.Info($"Order Placed for 1 {message.Ship}.");
            Data.NumberInInventory -= 1;
            log.Info($"Inventory for {Data.Ship} now at {Data.NumberInInventory}");
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShipInventoryData> mapper)
        {
            mapper.ConfigureMapping<InitializeShipInventory>(x => x.Ship).ToSaga(x => x.Ship);
            mapper.ConfigureMapping<OrderPlaced>(x => x.Ship).ToSaga(x => x.Ship);
        }

        public void Handle(InitializeShipInventory message)
        {
            Data.Ship = message.Ship;
            Data.NumberInInventory = message.InventoryCount;
        }
    }
}