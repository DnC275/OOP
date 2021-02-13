using System;
using System.Collections.Generic;

namespace Backups2
{
    public abstract class LimitType
    {
        public abstract int CompareLimitWithList(LinkedList<RestorePoint> pointsList);

        public abstract bool CheckAddPoint(RestorePoint point = null);

        public abstract void ResetValueAtMoment();

        public abstract string GetName();
        
        //returns false if function had to add additional points
        public bool ApplyLimit(LinkedList<RestorePoint> pointsList, out LinkedList<RestorePoint> newPointsList)
        {
            newPointsList = new LinkedList<RestorePoint>();
            if (CompareLimitWithList(pointsList) >= 0)
            {
                var node = pointsList.Last;
                while (node != null && CheckAddPoint(node.Value))
                {
                    newPointsList.AddFirst(node.Value);
                    node = node.Previous;
                }

                node = node == null ? pointsList.First : node.Next;
                ResetValueAtMoment();
                if (node.Value is DeltaRestorePoint)
                {
                    while (node != null && node.Value is DeltaRestorePoint)
                    {
                        node = node.Previous;
                        newPointsList.AddFirst(node.Value);
                    }
                    return false;
                }
                return true;
            }
            newPointsList = new LinkedList<RestorePoint>(pointsList);
            return true;
        }
    }

    public class CountLimit : LimitType
    {
        private int limitCount;
        private int countAtMoment;

        public int LimitCount => limitCount;

        public override string GetName() => $"Count - {limitCount}";

        public CountLimit(int count)
        {
            limitCount = count;
            countAtMoment = 0;
        }

        public override int CompareLimitWithList(LinkedList<RestorePoint> pointsList)
        {
            if (pointsList.Count == limitCount)
                return 0;
            return pointsList.Count < limitCount ? -1 : 1;
        }

        public override bool CheckAddPoint(RestorePoint point = null)
        {
            countAtMoment++;
            return countAtMoment <= limitCount;
        }

        public override void ResetValueAtMoment() => countAtMoment = 0;
    }
    
    public class SizeLimit : LimitType
    {
        private long limitSize;
        private long sizeAtMoment;

        public long LimitSize => limitSize;
        
        public override string GetName() => $"Size - {limitSize}";

        public SizeLimit(long size)
        {
            limitSize = size;
            sizeAtMoment = 0;
        }

        public override int CompareLimitWithList(LinkedList<RestorePoint> pointsList)
        {
            long tmpSize = 0;
            foreach (var point in pointsList)
            {
                tmpSize += point.Size;
            }
            if (tmpSize == limitSize)
                return 0;
            return tmpSize < limitSize ? -1 : 1;
        }

        public override bool CheckAddPoint(RestorePoint point)
        {
            sizeAtMoment += point.Size;
            return sizeAtMoment <= limitSize;
        }
        
        public override void ResetValueAtMoment() => sizeAtMoment = 0;
    }
    
    public class DateLimit : LimitType
    {
        private DateTime limitDate;
        //private long dateAtMoment;

        public DateTime LimitDate => limitDate;
        
        public override string GetName() => $"Date - {limitDate}";
        
        public DateLimit(DateTime date)
        {
            limitDate = date;
            //dateAtMoment = 0;
        }

        public override int CompareLimitWithList(LinkedList<RestorePoint> pointsList)
        {
            return DateTime.Compare(pointsList.First.Value.CreationTime, limitDate);
        }

        public override bool CheckAddPoint(RestorePoint point)
        {
            if (DateTime.Compare(point.CreationTime, limitDate) >= 0)
            {
                return true;
            }
            return false;
        }

        public override void ResetValueAtMoment()
        {
        }
    }
}