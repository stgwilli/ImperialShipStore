using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace SalesEndpoint.Handlers
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private static readonly ILog log = LogManager.GetLogger<PlaceOrderHandler>();
        private readonly IBus bus;

        public PlaceOrderHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(PlaceOrder message)
        {
            log.Info($"Order for Ship {message.Ship} has been placed with Id: {message.Id}");
            log.Info($"Publishing OrderPlaced Event");

            var orderPlaced = new OrderPlaced
            {
                OrderId = message.Id,
                Ship = message.Ship
            };
            
            bus.Publish(orderPlaced);
        }
    }
}