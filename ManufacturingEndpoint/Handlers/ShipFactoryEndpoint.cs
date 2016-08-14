using System;
using System.Threading;
using Messages.Commands;
using Messages.Events;
using NServiceBus;

namespace ManufacturingEndpoint.Handlers
{
    public class ShipFactoryEndpoint : IHandleMessages<ReplishInventory>
    {
        private readonly IBus bus;

        public ShipFactoryEndpoint(Schedule schedule, IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(ReplishInventory message)
        {
            Thread.Sleep(10);  
            bus.Send("ImperialShipStore.Inventory", new )
        }
    }
}