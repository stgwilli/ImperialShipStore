using NServiceBus;

namespace Messages.Commands
{
    public class ReplishInventory : ICommand
    {
        public string Ship { get; set; }
    }
}