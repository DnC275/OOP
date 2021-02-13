using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using BLL;
using BLL;
using DAL;
using DAL.Entities;
using Data;

namespace Reports
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeService employeeService = new EmployeeService(employeeRepository);
            EmployeeController employeeController = new EmployeeController(employeeService);
            
            
            
            TaskRepository taskRepository = new TaskRepository();
            TaskChangeRepository taskChangeRepository = new TaskChangeRepository();
            TaskService taskService = new TaskService(taskRepository, taskChangeRepository, employeeService);
            TaskController taskController = new TaskController(taskService);

            int firstId = employeeController.AddEmployee("1st");
            int secId = employeeController.AddEmployee("2nd", firstId);
            int thirdId = employeeController.AddEmployee("3rd", firstId);
            
            employeeController.ChangeDirector(thirdId, secId);
            //employeeController.ChangeDirector(secId, thirdId);
            
            int firstTaskId = taskController.AddTask("first task", "first task description");
            int secondTaskId = taskController.AddTask("second task", "second task description");
            taskController.AssignEmployee(firstTaskId, firstId);
            taskController.AssignEmployee(secondTaskId, secId);
            taskController.AddComment(firstTaskId, firstId, "comment text");
            taskController.CompleteTask(firstTaskId, firstId);
            List<ViewTaskModel> list1 = taskController.GetSubordinatesTasks(firstId);
            List<ViewTaskModel> list2 = taskController.GetTasksByEmployee(firstId);
            List<ViewTaskModel> list3 = taskController.GetTasksModifiedByEmployee(firstId);
            
            ReportRepository reportRepository = new ReportRepository();
            ReportService reportService = new ReportService(reportRepository, taskService);
            ReportController reportController = new ReportController(reportService);
            int firstReportId = reportController.CreateReport(firstId, "first report text");
            reportController.UpdateText(firstReportId, "first report fixed text");
            //MyDate.date = MyDate.date.AddDays(1);
            reportController.UpdateText(firstReportId, "first report twice fixed text");
            List<ViewReportModel> list = reportController.GetReportsByEmployee(firstId);
            
            SprintReportRepository sprintReportRepository = new SprintReportRepository();
            SprintReportService sprintReportService = new SprintReportService(sprintReportRepository, employeeService, reportService);
            SprintReportController sprintReportController = new SprintReportController(sprintReportService);
            int firstSprintId = sprintReportController.CreateSprintReport(firstId, MyDate.GetDate().AddDays(2));
            MyDate.date = MyDate.date.AddDays(1);
            sprintReportController.AddDailyReport(firstSprintId, firstId, firstReportId);
        }
    }
}