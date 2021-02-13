using System;
using System.Collections.Generic;

namespace Backups2
{
    public class Cleaner
    {
        private LimitsMode limitsMode = LimitsMode.SuitableForSomeone;
        private LinkedList<LimitType> activeLimits = new LinkedList<LimitType>();

        public void AddLimit(LimitType limit)
        {
            foreach (var activeLimit in activeLimits)
            {
                if (activeLimit.GetType().Equals(limit.GetType()))
                {
                    activeLimits.Remove(activeLimit);
                    activeLimits.AddLast(limit);
                    return;
                }
            }
            activeLimits.AddLast(limit);
        }

        public void ResetLimits() => activeLimits.Clear();

        public void SetLimitsMode(LimitsMode mode) => limitsMode = mode;

        public bool Apply(LinkedList<RestorePoint> points, out LinkedList<RestorePoint> newPoints)
        {
            if (activeLimits.Count == 0)
            {
                newPoints = points;
                return true;
            }
            LinkedList<RestorePoint> firstList = null;
            LinkedList<RestorePoint> secondList;
            bool firstCheck = false, secondCheck = false;
            foreach (var limitType in activeLimits)
            {
                secondCheck = limitType.ApplyLimit(points, out secondList);
                if (firstList != null)
                {
                    if ((limitsMode == LimitsMode.SuitableForSomeone && firstList.Count > secondList.Count) ||
                        (limitsMode == LimitsMode.SuitableForEveryone && firstList.Count < secondList.Count))
                    {
                        firstList = secondList;
                        firstCheck = secondCheck;
                    }
                    else if (firstList.Count == secondList.Count)
                    {
                        if (limitsMode == LimitsMode.SuitableForEveryone)
                        {
                            firstCheck = firstCheck || secondCheck;
                        }
                        else
                        {
                            firstCheck = firstCheck && secondCheck;
                        }
                    }
                }
                else
                {
                    firstList = secondList;
                    firstCheck = secondCheck;
                }
            }
            newPoints = firstList;
            return firstCheck;
        }

        public List<string> GetLimitsInfo()
        {
            var ans = new List<string>();
            if (limitsMode == LimitsMode.SuitableForEveryone)
            {
                ans.Add($"Mode - Delete a point if it fits at least one limit");
            }
            else
            {
                ans.Add($"Mode - Delete a point if it fits all the limits");
            }
            foreach (var tmp in activeLimits)
            {
                ans.Add(tmp.GetName());
            }
            return ans;
        }
    }
}