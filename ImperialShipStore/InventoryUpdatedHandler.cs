using Messages.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialShipStore
{
    public class InventoryUpdatedHandler : IHandleMessages<InventoryUpdated>
    {
        public void Handle(InventoryUpdated message)
        {
            ShipRepository.Ships.First(x => x.Ship == message.Ship).Count = message.Count;
        }
    }
}
