using System;
using System.Collections.Generic;

namespace Races
{
    public interface IDistanceReducer
    {
        public double RaceTime(double distance, double speed);
    }
    
    public abstract class BaseDistanceReducer
    {
        public double RaceTime(double distance, double speed, double percent)
        {
            return (distance - distance * percent / 100) / speed;
        }
    }

    public class IntervalReduce : BaseDistanceReducer, IDistanceReducer
    {
        private List<Border_Percent> borders;
        private double lastPercentReduction;

        public IntervalReduce(double lastPercentReduction, params Border_Percent[] parameters)
        {
            this.lastPercentReduction = lastPercentReduction;
            borders = new List<Border_Percent>();
            foreach (var tmp in parameters)
            {
                borders.Add(tmp);
            }
        }

        public double RaceTime(double distance, double speed)
        {
            for (int i = 0; i < borders.Count; i++)
            {
                if (distance <= borders[i].upperBorder)
                {
                    return base.RaceTime(distance, speed, borders[i].percentReduction);
                }
            }
            return base.RaceTime(distance, speed, lastPercentReduction);
        }
    }

    public class StaticReduce : BaseDistanceReducer, IDistanceReducer
    {
        private double percentReduction;
        
        public StaticReduce(double percentReduction)
        {
            this.percentReduction = percentReduction;
        }

        public double RaceTime(double distance, double speed)
        {
            return base.RaceTime(distance, speed, percentReduction);
        }
    }

    public class UniformReduce : BaseDistanceReducer, IDistanceReducer
    {
        private double interval;
        private double percentReduction;

        public UniformReduce(double percentReduction, double interval)
        {
            this.percentReduction = percentReduction;
            this.interval = interval;
        }

        public double RaceTime(double distance, double speed)
        {
            double ansTime = 0.0;
            while ((int) (distance / interval) > 0)
            {
                
                ansTime += interval / speed;
                distance -= distance * (percentReduction / 100.0);
                distance -= interval;
            }
            ansTime += Math.Max(distance, 0) / speed;
            return ansTime;
        }
    }

    public struct Border_Percent
    {
        public double upperBorder;
        public double percentReduction;

        public Border_Percent(double upperBorder, double percentReduction)
        {
            this.upperBorder = upperBorder;
            this.percentReduction = percentReduction;
        }
    }
}