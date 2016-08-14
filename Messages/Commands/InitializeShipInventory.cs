using NServiceBus;

namespace Messages.Commands
{
    public class InitializeShipInventory : ICommand
    {
        public string Ship { get; set; }
        public int InventoryCount { get; set; }
    }
}