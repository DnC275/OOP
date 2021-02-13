using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class ReportDTO
    {
        private int id;
        private DateTime creationDate;
        private int employeeId;
        private List<int> completedTasks;
        private string text;

        public int Id => id;

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
        
        public ReportDTO(int id, int employeeId, DateTime creationDate, string text, params int[] p)
        {
            this.id = id;
            this.employeeId = employeeId;
            this.creationDate = creationDate;
            this.text = text;
            completedTasks = new List<int>(p);
        }

        public void AddCompletedTask(int taskId) => completedTasks.Add(taskId);

        public void UpdateText(string text) => this.text = text;
    }
}