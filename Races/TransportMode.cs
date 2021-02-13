using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Races
{
    public interface ITransport
    {
        public string Name { get; set; }
        
        public double RaceTime(double distance);
    }
    
    public abstract class LandTransport : ITransport
    {
        private string name;
        protected double speed;
        protected double restInterval;
        protected double[] restDuration;
        
        public string Name { get => name; set => name = value; }

        protected LandTransport(string name, double speed, double restInterval, params double[] restDuration)
        {
            Name = name;
            this.speed = speed;
            this.restInterval = restInterval;
            this.restDuration = restDuration;
        }

        public double RaceTime(double distance)
        {
            double result = 0;
            int stopCount = Convert.ToInt32(distance / (speed * restInterval));
            result += restInterval * stopCount;
            distance -= speed * restInterval * stopCount;
            if (distance == 0)
                stopCount -= 1;
            for (int i = 0; i < stopCount; i++)
            {
                if (i < restDuration.Length - 1)
                {
                    result += restDuration[i];
                }
                else
                    result += restDuration[restDuration.Length - 1];
            }
            return result + distance / speed;
        }
    }

    public abstract class AirTransport : ITransport
    {
        private string name;
        protected double speed;
        protected IDistanceReducer distanceReducer;

        public string Name { get => name; set => name = value; }
        
        protected AirTransport(string name, double speed, IDistanceReducer distanceReducer)
        {
            Name = name;
            this.speed = speed;
            this.distanceReducer = distanceReducer;
        }
        
        public double RaceTime(double distance)
        {
            return distanceReducer.RaceTime(distance, speed);
        }
    }
}