using System;
using System.Collections.Generic;
using System.Net.Sockets;
using BLL.DTO;
using BLL.Infrastructure;
using DAL;
using DAL.Entities;
using DAL.Infrastructure;
using Data;
using Exceptions;

namespace BLL
{
    public class ReportService : IReportService
    {
        private IRepository<Report> repository;
        private IService<TaskDTO, Task> taskService;

        public ReportService(IRepository<Report> repository, IService<TaskDTO, Task> taskService)
        {
            this.repository = repository;
            this.taskService = taskService;
        }

        public int Add(ReportDTO entity)
        {
            return repository.Create(ConvertBblToDal(entity));
        }

        public Dictionary<int, ReportDTO> GetAll()
        {
            return null;
        }

        public List<ReportDTO> GetReportsByEmployee(int employeeId)
        {
            Dictionary<int, Report> dict = repository.GetAll();
            List<ReportDTO> res_list = new List<ReportDTO>();
            foreach (var report in dict.Values)
            {
                if (report.EmployeeId == employeeId)
                {
                    res_list.Add(ConvertDalToBbl(report));
                }
            }
            return res_list;
        }

        public ReportDTO Get(int reportId)
        {
            return ConvertDalToBbl(repository.GetById(reportId));
        }

        public void AddCompletedTask(int reportId, int taskId)
        {
            if (!taskService.CheckExistence(taskId))
                throw new NonexistentTask();
            TaskDTO taskDto = taskService.Get(taskId);
            if (taskDto.Status != Task.TaskStatus.Resolved)
                throw new NonresolvedTask();
            ReportDTO reportDto = ConvertDalToBbl(repository.GetById(reportId));
            if (MyDate.GetDate().Day != reportDto.CreationDate.Day ||
                MyDate.GetDate().Month != reportDto.CreationDate.Month ||
                MyDate.GetDate().Year != reportDto.CreationDate.Year)
            {
                throw new TaskTimeIsOver();
            }
            if (reportDto.GetCompletedTasks().Contains(taskId))
                throw new TaskAlreadyCompleted();
            reportDto.AddCompletedTask(taskId);
            repository.Update(ConvertBblToDal(reportDto));
        }

        public void UpdateText(int reportId, string text)
        {
            ReportDTO reportDto = ConvertDalToBbl(repository.GetById(reportId));
            if (MyDate.GetDate().Day != reportDto.CreationDate.Day ||
                MyDate.GetDate().Month != reportDto.CreationDate.Month ||
                MyDate.GetDate().Year != reportDto.CreationDate.Year)
            {
                throw new ReportTimeIsOver();
            }
            reportDto.UpdateText(text);
            repository.Update(ConvertBblToDal(reportDto));
        }

        public bool CheckForCompleteness(int reportId)
        {
            ReportDTO reportDto = ConvertDalToBbl(repository.GetById(reportId));
            return reportDto.CreationDate.Day != MyDate.GetDate().Day ||
                   reportDto.CreationDate.Month != MyDate.GetDate().Month ||
                   reportDto.CreationDate.Year != MyDate.GetDate().Year;
        }

        public ReportDTO ConvertDalToBbl(Report entity)
        {
            return new ReportDTO(entity.Id, entity.EmployeeId, entity.CreationDate, entity.Text, entity.GetCompletedTasks().ToArray());
        }

        public Report ConvertBblToDal(ReportDTO entity)
        {
            return new Report(entity.Id, entity.EmployeeId, entity.CreationDate, entity.Text, entity.GetCompletedTasks().ToArray());
        }
        
        public bool CheckExistence(int id)
        {
            return repository.CheckIdExistence(id);
        }
    }
}