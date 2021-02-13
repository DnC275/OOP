using System;
using System.Collections.Generic;
using System.IO;
using Backups2.MyExceptions;

namespace Backups2
{
    public class Backup
    {
        private static int commonId = 1;
        private int restorePointId = 1;
        private int id;
        private string name;
        private DateTime creationTime;
        private LinkedList<string> trackedFiles;
        private LinkedList<RestorePoint> points;
        private Cleaner cleaner;
        
        public DateTime CreationTime => creationTime;
        public string Name => name != null ? name : $"Backup {id}";
        public List<string> GetFiles() => new List<string>(trackedFiles);

        public List<string> GetPointFiles(string pointName)
        {
            foreach (var point in points)
            {
                if (point.Name == pointName)
                    return point.GetFiles();
            }
            throw new NonexistentPointException(pointName);
        }

        public List<string> GetNamesOfPoints()
        {
            LinkedList<string> names = new LinkedList<string>();
            foreach (var point in points)
            {
                names.AddLast(point.Name);
            }
            return new List<string>(names);
        }
        public long BackupSize
        {
            get
            {
                long size = 0;
                foreach (RestorePoint point in points)
                {
                    size += point.Size;
                }
                return size;
            }
        }
        
        public Backup(string name = null)
        {
            id = commonId++;
            this.name = name;
            creationTime = DateTime.Now;
            points = new LinkedList<RestorePoint>();
            trackedFiles = new LinkedList<string>();
            cleaner = new Cleaner();
        }
        
        public Backup(string name, params string[] paths) : this(name)
        {
            foreach (string path in paths)
            {
                var file = new FileInfo(path);
                if (file.Exists && !trackedFiles.Contains(file.FullName))
                {
                    trackedFiles.AddLast(file.FullName);
                }
            }
        }
        
        public void CreateRestorePoint(string pointName = null)
        {
            points.AddLast(new RestorePoint(restorePointId, pointName ?? $"Restore point {restorePointId}", trackedFiles));
            restorePointId++;
            if (!cleaner.Apply(points, out var newPoints))
                throw new SomePointsNotDeletedException();
            points = newPoints;
        }
        
        public void CreateDeltaRestorePoint(string pointName = null)
        {
            if (points.Count == 0)
                throw new FirstDeltaPointException();
            points.AddLast(new DeltaRestorePoint(restorePointId, pointName ?? $"Delta restore point {restorePointId}", points.Last.Value));
            restorePointId++;
            if (!cleaner.Apply(points, out var newPoints))
                throw new SomePointsNotDeletedException();
            points = newPoints;
        }

        public void SetCountLimit(int count)
        {
            cleaner.AddLimit(new CountLimit(count));
            if (!cleaner.Apply(points, out var newPoints))
                throw new SomePointsNotDeletedException();
            points = newPoints;
        }
        
        public void SetSizeLimit(long size)
        {
            cleaner.AddLimit(new SizeLimit(size));
            if (!cleaner.Apply(points, out var newPoints))
                throw new SomePointsNotDeletedException();
            points = newPoints;
        }
        
        public void SetDateLimit(int year, int month, int day)
        {
            cleaner.AddLimit(new DateLimit(new DateTime(year, month, day)));
            if (!cleaner.Apply(points, out var newPoints))
                throw new SomePointsNotDeletedException();
            points = newPoints;
        }

        public void SetLimitsMode(LimitsMode mode)
        { 
            cleaner.SetLimitsMode(mode);
            if (!cleaner.Apply(points, out var newPoints))
                throw new SomePointsNotDeletedException();
            points = newPoints;
        }

        public void ResetLimits() => cleaner.ResetLimits();

        public void AddFile(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            if (!fileInfo.Exists) 
                throw new NonexistentFileException(file);
            if (trackedFiles.Find(fileInfo.FullName) != null) 
                throw new FileAlreadyTrackedException(file);
            trackedFiles.AddLast(fileInfo.FullName);
        }

        public void DeleteFile(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            if (trackedFiles.Find(fileInfo.FullName) == null) 
                throw new FileAlreadyNotTrackedException(file);
            trackedFiles.AddLast(fileInfo.FullName);
        }

        public List<string> GetLimitsInfo() => cleaner.GetLimitsInfo();
    }
}