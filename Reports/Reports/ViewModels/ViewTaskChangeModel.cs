using System;

namespace Reports
{
    public class ViewTaskChangeModel
    {
        private int id;
        private int taskId;
        private int employeeId;
        private string changeText;
        private DateTime changeDate;

        public int Id => id;

        public int TaskId => taskId;

        public int EmployeeId => employeeId;

        public string ChangeText => changeText;

        public DateTime ChangeDate => changeDate;

        public ViewTaskChangeModel(int id, int taskId, int employeeId, string changeText, DateTime changeDate)
        {
            this.id = id;
            this.taskId = taskId;
            this.employeeId = employeeId;
            this.changeText = changeText;
            this.changeDate = changeDate;
        }
    }
}