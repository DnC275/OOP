using System;
using System.Buffers.Text;
using System.Collections.Generic;

namespace Races
{
    public abstract class TRace
    {
        protected List<ITransport> transports;
        private readonly double distance;

        public TRace(double dist)
        {
            distance = dist;
            transports = new List<ITransport>();
        }

        protected void AddTransport<T>(params ITransport[] transportsParam)
        {
            foreach (var transport in transportsParam)
            {
                if (transport is T)
                    transports.Add(transport);
                else
                    throw new MyException(transport);
            }
        }

        public List<ITransport> Start()
        {
            double minTime = 1e9;
            List<ITransport> winners = new List<ITransport>();
            foreach (var tmp in transports)
            {
                double raceTime = tmp.RaceTime(distance);
                if (raceTime < minTime)
                {
                    minTime = raceTime;
                    winners.Clear();
                    winners.Add(tmp);
                }
                else if (raceTime == minTime)
                {
                    winners.Add(tmp);
                }
            }
            //Console.WriteLine("---------");
            return winners;
        }
    }

    public class LandRace : TRace
    {
        public LandRace(double distance) : base(distance)
        {
        }

        public void AddTransport(params ITransport[] transportsParam)
        {
            try
            {
                base.AddTransport<LandTransport>(transportsParam);
            }
            catch (MyException ex)
            {
                Console.WriteLine($"Класс транспорта {ex.transport.Name} не соответствует типу гонки(данная гонка только для наземных транспортных средств)");
            }
        }
    }
    
    public class AirRace : TRace
    {
        public AirRace(double distance) : base(distance)
        {
        }
        
        public void AddTransport(params ITransport[] transportsParam)
        {
            try
            {
                base.AddTransport<AirTransport>(transportsParam);
            }
            catch (MyException ex)
            {
                Console.WriteLine($"Класс транспорта {ex.transport.Name} не соответствует типу гонки(данная гонка только для воздушных транспортных средств)");
            }
        }
    }

    public class Race : TRace
    {
        public Race(double distance) : base(distance) {}

        public void AddTransport(params ITransport[] transportsParam)
        {
            foreach (var transport in transportsParam)
            {
                transports.Add(transport);  
            }
        }
    }
}