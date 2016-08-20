using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialShipStore
{
    public static class ShipRepository
    {
        public static List<ShipEntry> Ships = new List<ShipEntry>
        {
            new ShipEntry("TIE Fighter", 5),
            new ShipEntry("TIE Avenger", 5),
            new ShipEntry("TIE Bomber", 5),
            new ShipEntry("Imperial Class Star Destroyer", 5),
            new ShipEntry("Dreadnaught", 5),
            new ShipEntry("Marauder-class Corvette", 5),
            new ShipEntry("Super Star Destroyer", 5)
        };
    }

    public class ShipEntry
    {
        public string Ship { get; set; }
        public int Count { get; set; }

        public ShipEntry(string ship, int count)
        {
            this.Ship = ship;
            this.Count = count;
        }
    }
}
