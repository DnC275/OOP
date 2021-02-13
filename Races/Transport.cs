using System.Data;
using System.Security;

namespace Races
{
    public class BactrianCamel : LandTransport
    {
        public BactrianCamel(string name) : base(name, 10, 30, 5, 8)
        {
        }
    }

    public class FleetCamel : LandTransport
    {
        public FleetCamel(string name) : base( name, 40, 10, 5, 6.5, 8)
        {
        }
    }

    public class Centaur : LandTransport
    {
        public Centaur(string name) : base(name, 15, 8, 2)
        {
        }
    }

    public class RoverBoots : LandTransport
    {
        public RoverBoots(string name) : base(name, 6, 60, 10, 5)
        {
        }
    }

    public class MagicCarpet : AirTransport
    {
        public MagicCarpet(string name) : base(name, 10, 
            new IntervalReduce(5, new Border_Percent(1000, 0), 
                new Border_Percent(5000, 3), new Border_Percent(10000, 10)))
        {
        }
    }

    public class Stupa : AirTransport
    {
        public Stupa(string name) : base(name, 8, new StaticReduce(6))
        {
        }
    }

    public class Broom : AirTransport
    {
        public Broom(string name) : base(name, 20, new UniformReduce(1, 1000))
        {
        }
    }
}