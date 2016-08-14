using System;
using NServiceBus.Saga;

namespace InventoryEndpoint.Handlers
{
    public class ShipInventoryData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string Ship { get; set; }
        public int NumberInInventory { get; set; }
    }
}