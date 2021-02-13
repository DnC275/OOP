using System;
using System.Collections.Generic;
using DAL.Infrastructure;

namespace DAL.Entities
{
    public class Report : IEntity
    {
        private int id;
        private DateTime creationDate;
        private int employeeId;
        private List<int> completedTasks;
        private string text;

        public int Id { 
            get => id;
            internal set => id = value;
        }

        public DateTime CreationDate
        {
            get => creationDate;
            internal set => creationDate = value;
        }

        public int EmployeeId
        {
            get => employeeId;
            internal set => employeeId = value;
        }

        public List<int> GetCompletedTasks()
        {
            return new List<int>(completedTasks);
        }

        public string Text
        {
            get => text;
            internal set => text = value;
        }

        public Report(int id, int employeeId, DateTime creationDate, string text, params int[] p)
        {
            this.id = id;
            this.employeeId = employeeId;
            this.creationDate = creationDate;
            this.text = text;
            completedTasks = new List<int>(p);
        }
    }
}