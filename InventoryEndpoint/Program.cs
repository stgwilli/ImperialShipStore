using System;
using Messages.Commands;
using NServiceBus;

namespace InventoryEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Imperial Ship Store - Inventory Endpoint";
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("ImperialShipStore.Inventory");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                Console.Clear();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}