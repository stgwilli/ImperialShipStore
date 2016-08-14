using System;
using NServiceBus;

namespace ManufacturingEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Imperial Ship Store - Manufactruing Endpoint";
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("ImperialShipStore.Manufacturing");
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
