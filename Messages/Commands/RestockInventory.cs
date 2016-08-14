using NServiceBus;

namespace Messages.Commands
{
    public class RestockInventory : ICommand
    {
        public string Ship { get; set; }
        public int Amount { get; set; }
    }
}