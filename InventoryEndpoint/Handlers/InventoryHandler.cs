using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;

namespace InventoryEndpoint.Handlers
{
    public class InventoryHandler : Saga<ShipInventoryData>
        , IAmStartedByMessages<InitializeShipInventory>
        , IHandleMessages<OrderPlaced>
        , IHandleMessages<RestockInventory>
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
            Bus.Publish(new InventoryUpdated { Ship = Data.Ship, Count = Data.NumberInInventory });

            if (Data.NumberInInventory < 3)
            {
                log.Warn($"Low Inventory for {Data.Ship}.");
                bus.Publish(new LowInventory {Ship = Data.Ship});
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShipInventoryData> mapper)
        {
            mapper.ConfigureMapping<InitializeShipInventory>(x => x.Ship).ToSaga(x => x.Ship);
            mapper.ConfigureMapping<OrderPlaced>(x => x.Ship).ToSaga(x => x.Ship);
            mapper.ConfigureMapping<RestockInventory>(x => x.Ship).ToSaga(x => x.Ship);
        }

        public void Handle(InitializeShipInventory message)
        {
            Data.Ship = message.Ship;
            Data.NumberInInventory = message.InventoryCount;
        }

        public void Handle(RestockInventory message)
        {
            log.Info($"Received shipment of {message.Amount} new {message.Ship}.");
            Data.NumberInInventory += message.Amount;
            log.Info($"Inventory of {Data.Ship} now at {Data.NumberInInventory}");
            Bus.Publish(new InventoryUpdated { Ship = Data.Ship, Count = Data.NumberInInventory });
        }
    }
}