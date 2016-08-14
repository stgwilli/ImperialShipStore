using System;
using NServiceBus;

namespace Messages.Commands
{
    public class PlaceOrder : ICommand
    {
        public Guid Id { get; set; }
        public string Ship { get; set; }
    }
}