using System;
using System.Threading;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace ManufacturingEndpoint.Handlers
{
    public class ShipFactoryEndpoint : IHandleMessages<LowInventory>
    {
        private static readonly ILog log = LogManager.GetLogger<ShipFactoryEndpoint>();
        private readonly IBus bus;
        private Random rnd = new Random();

        public ShipFactoryEndpoint(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(LowInventory message)
        {
            int numberOfShipsToBuild = rnd.Next(1, 5);
            log.Info($"Low inventory for {message.Ship}. Starting construction of {numberOfShipsToBuild} new {message.Ship}");

            // sleep 10 seconds
            Thread.Sleep(10000);
            bus.Send("ImperialShipStore.Inventory",
                new RestockInventory {Ship = message.Ship, Amount = numberOfShipsToBuild});
            log.Info($"Completed building {numberOfShipsToBuild} {message.Ship}.");
        }
    }
}