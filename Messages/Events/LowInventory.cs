using NServiceBus;

namespace Messages.Events
{
    public class LowInventory : IEvent
    {
        public string Ship { get; set; }
    }
}