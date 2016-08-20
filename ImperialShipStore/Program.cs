using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages.Commands;
using NServiceBus;

namespace ImperialShipStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Imperial Ship Store";
            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("ImperialShipStore.Client");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();    // Create Message Queues automatically
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                foreach (var entry in ShipRepository.Ships)
                    bus.Send("ImperialShipStore.Inventory", new InitializeShipInventory { Ship = entry.Ship, InventoryCount = entry.Count });

                SendOrder(bus);
            }
        }

        static void SendOrder(IBus bus)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Imperial Ship Store!");
            var input = DisplayShipList();

            while (input != "q")
            {
                if (input != "r") {
                    var selection = int.Parse(input);
                    var id = Guid.NewGuid();
                    var placeOrder = new PlaceOrder
                    {
                        Id = id,
                        Ship = ShipRepository.Ships[selection - 1].Ship
                    };

                    bus.Send("ImperialShipStore.Sales", placeOrder);
                    Console.WriteLine($"Order for {placeOrder.Ship} has been placed with Id {placeOrder.Id}.");
                    Console.WriteLine();
                }

                input = DisplayShipList();
            }
        }

        static string DisplayShipList()
        {
            Console.WriteLine("Select a ship below to purchase (r to reload, q to quit):");
            for (var i = 0; i < ShipRepository.Ships.Count; i++)
            {
                Console.WriteLine($"{i+1}. {ShipRepository.Ships[i].Ship} ({ShipRepository.Ships[i].Count} in stock)");
            }
            return Console.ReadLine();
        }
    }
}
