using System;
using System.Collections.Generic;
using DAL.Infrastructure;
using Data;

namespace DAL.Entities
{
    public class Task : IEntity
    {
        private int id;
        private string name;
        private string description;
        private List<string> comments;
        private int employeeId;
        private TaskStatus status;
        private DateTime creationDate;
        private DateTime lastModifiedDate;

        public int Id { get => id; internal set => id = value; }
        
        public string Name { get => name; }

        public DateTime CreationDate
        {
            get => creationDate;
            internal set => creationDate = value;
        }

        public DateTime LastModifiedDate
        {
            get => lastModifiedDate;
            internal set => lastModifiedDate = value;
        }

        public string Description => description;

        public int EmployeeId => employeeId;

        public TaskStatus Status
        {
            get => status;
            internal set => status = value;
        }

        public List<string> GetComments() => new List<string>(comments);

        public Task(int id, string name, string description, TaskStatus status, DateTime creationDate, DateTime lastModifiedDate, int employeeId, params string[] comments)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.employeeId = employeeId;
            this.status = status;
            this.comments = new List<string>(comments);
            this.creationDate = creationDate;
            this.lastModifiedDate = lastModifiedDate;
        }

        public enum TaskStatus
        {
            Open,
            Active,
            Resolved
        }
    }
}