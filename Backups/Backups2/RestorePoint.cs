using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Backups2
{
    public class RestorePoint
    {
        protected int id;
        protected string name;
        protected LinkedList<string> files;
        //protected LinkedList<long> sizesOfFiles;
        protected DateTime creationTime;
        protected long size;

        public int Id => id;
        
        public long Size => size;

        public DateTime CreationTime => creationTime;

        public string Name => name;
        
        public List<string> GetFiles() => new List<string>(files);
        
        protected RestorePoint(int id, string name)
        {
            this.id = id;
            this.name = name;
            creationTime = DateTime.Now;
        }
        
        public RestorePoint(int id, string name, LinkedList<string> pathsOfFiles) : this(id, name)
        {
            files = new LinkedList<string>();
            foreach (var pathOfFile in pathsOfFiles)
            {
                FileInfo pointFile = new FileInfo(pathOfFile);
                size += pointFile.Exists ? pointFile.Length : 0;
                files.AddLast(pointFile.FullName);
            }
        }
    }
    
    public class DeltaRestorePoint : RestorePoint
    {
        private RestorePoint parentPoint;

        public RestorePoint ParentPoint => parentPoint;

        public DeltaRestorePoint(int id, string name, RestorePoint parentPoint) : base(id, name)
        {
            this.parentPoint = parentPoint;
            files = new LinkedList<string>();
            foreach (var file in parentPoint.GetFiles())
            {
                FileInfo pointFile = new FileInfo(file);
                if (pointFile.Exists && (DateTime.Compare(parentPoint.CreationTime, pointFile.LastWriteTime) < 0))
                {
                    files.AddLast(pointFile.FullName);
                    size += pointFile.Length;
                }
            }
        }
    }
}