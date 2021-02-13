using System;
using System.Collections.Generic;

namespace Races
{
    class Program
    {
        static void Main(string[] args)
        {
            BactrianCamel bactrianCamel = new BactrianCamel("Camel");
            Centaur centaur = new Centaur("Centaur");
            FleetCamel fleetCamel = new FleetCamel("Fast camel");
            RoverBoots roverBoots = new RoverBoots("Magic boots");
            MagicCarpet carpet = new MagicCarpet("MagicCarpet");
            Stupa stupa = new Stupa("Stupa");
            Broom broom = new Broom("Broom");

            LandRace landRace = new LandRace(10000);
            landRace.AddTransport(bactrianCamel, centaur, roverBoots, fleetCamel);
            List<ITransport> winners = landRace.Start();
            if (winners.Count == 1)
            {
                Console.WriteLine($"Победит {winners[0].Name}");
                Console.WriteLine("---------");
            }
            Console.WriteLine("Победят следующие участники:");
            foreach (var tmp in winners)
            {
                Console.WriteLine(tmp.Name);
            }
            Console.WriteLine("---------");
            
            AirRace airRace = new AirRace(50000);
            airRace.AddTransport(carpet, stupa, broom);
            winners = airRace.Start();
            if (winners.Count == 1)
            {
                Console.WriteLine($"Победит {winners[0].Name}");
                Console.WriteLine("---------");
            }
            Console.WriteLine("Победят следующие участники:");
            foreach (var tmp in winners)
            {
                Console.WriteLine(tmp.Name);
            }
            Console.WriteLine("---------");
            
            Race race = new Race(15000);
            race.AddTransport(fleetCamel, centaur, broom, bactrianCamel, stupa, carpet, roverBoots);
            winners = race.Start();
            if (winners.Count == 1)
            {
                Console.WriteLine($"Победит {winners[0].Name}");
                Console.WriteLine("---------");
            }
            Console.WriteLine("Победят следующие участники:");
            foreach (var tmp in winners)
            {
                Console.WriteLine(tmp.Name);
            }
            Console.WriteLine("---------");
        }
    }
}
