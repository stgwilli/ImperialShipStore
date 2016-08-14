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
        private static List<string> _ships = new List<string>()
        {
            "TIE Fighter",
            "Imperial Star Destroyer",
            "AT-AT Walker",
            "Death Star"
        };

        static void Main(string[] args)
        {
            Console.Title = "Imperial Ship Store";

            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("ImperialShipStore.Client");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.EnableInstallers();    // Create Message Queues automatically
            busConfiguration.UsePersistence<InMemoryPersistence>();

            var rnd = new Random();

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                foreach (var ship in _ships)
                {
                    bus.Send("ImperialShipStore.Inventory", new InitializeShipInventory { Ship = ship, InventoryCount = rnd.Next(2, 5)});
                }
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
                var selection = int.Parse(input);
                var id = Guid.NewGuid();
                var placeOrder = new PlaceOrder
                {
                    Id = id,
                    Ship = _ships[selection - 1]
                };

                bus.Send("ImperialShipStore.Sales", placeOrder);
                Console.WriteLine($"Order for {placeOrder.Ship} has been placed with Id {placeOrder.Id}.");
                Console.WriteLine();
                input = DisplayShipList();
            }
        }

        static string DisplayShipList()
        {
            Console.WriteLine("Select a ship below to purchase (q to quit):");
            for (var i = 0; i < _ships.Count; i++)
            {
                Console.WriteLine($"{i+1}. {_ships[i]}");
            }
            return Console.ReadLine();
        }
    }
}
