using System;
using DAL.Infrastructure;

namespace DAL.Entities
{
    public class TaskChange : IEntity
    {
        private int id;
        private int taskId;
        private int employeeId;
        private string changeText;
        private DateTime changeDate;
        
        public int Id { get => id; internal set => id = value; }

        public int TaskId => taskId;

        public int EmployeeId => employeeId;

        public string ChangeText => changeText;

        public DateTime ChangeDate
        {
            get => changeDate;
            internal set => changeDate = value;
        }

        public TaskChange(int id, int taskId, int employeeId, string changeText, DateTime changeDate)
        {
            this.id = id;
            this.taskId = taskId;
            this.employeeId = employeeId;
            this.changeText = changeText;
            this.changeDate = changeDate;
        }
    }
}