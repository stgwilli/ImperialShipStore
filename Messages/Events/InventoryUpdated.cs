using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
    public class InventoryUpdated : IEvent
    {
        public string Ship { get; set; }
        public int Count { get; set; }
    }
}
