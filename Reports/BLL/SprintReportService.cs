using System;
using System.Collections.Generic;
using BLL.DTO;
using BLL.Infrastructure;
using DAL;
using DAL.Entities;
using DAL.Infrastructure;
using Data;
using Exceptions;

namespace BLL
{
    public class SprintReportService : ISprintService
    {
        private IRepository<SprintReport> repository;
        private IEmployeeService employeeService;
        private IReportService reportService;

        public SprintReportService(IRepository<SprintReport> repository, IEmployeeService employeeService, IReportService reportService)
        {
            this.repository = repository;
            this.employeeService = employeeService;
            this.reportService = reportService;
        }
        
        public int Add(SprintReportDTO entity)
        {
            return repository.Create(ConvertBblToDal(entity));
        }

        public Dictionary<int, SprintReportDTO> GetAll()
        {
            return null;
        }

        public void AddDailyReport(int sprintId, int employeeId, int reportId)
        {
            if (!repository.CheckIdExistence(sprintId))
                throw new NonexistentSprint();
            if (!employeeService.CheckExistence(employeeId))
                throw new NonexistentEmployee();
            if (!reportService.CheckExistence(reportId))
                throw new NonexistentReport();
            if (!reportService.CheckForCompleteness(reportId))
                throw new ReportTimeIsOver();
            SprintReportDTO sprintReportDto = ConvertDalToBbl(repository.GetById(sprintId));
            if (DateTime.Compare(sprintReportDto.ClosingDate, MyDate.GetDate()) < 0)
                throw new SprintTimeIsOver();
            List<int> subordiatesId = new List<int>();
            employeeService.GetSubordinatesId(sprintReportDto.DirectorId, subordiatesId);
            if (employeeId == sprintReportDto.DirectorId || subordiatesId.Contains(employeeId))
            {
                if (sprintReportDto.GetReports().Contains(reportId))
                    throw new DailyReportAlreadyAdded();
                sprintReportDto.AddReport(reportId);
                repository.Update(ConvertBblToDal(sprintReportDto));
                return;
            }
            throw new InsufficientRights();
        }

        public SprintReportDTO ConvertDalToBbl(SprintReport entity)
        {
            return new SprintReportDTO(entity.Id, entity.DirectorId, entity.CreationDate, entity.ClosingDate, entity.Text, entity.GetReports());
        }

        public SprintReport ConvertBblToDal(SprintReportDTO entity)
        {
            return new SprintReport(entity.Id, entity.DirectorId, entity.CreationDate, entity.ClosingDate, entity.Text, entity.GetReports());
        }
        
        public bool CheckExistence(int id)
        {
            return repository.CheckIdExistence(id);
        }

        public SprintReportDTO Get(int id)
        {
            return ConvertDalToBbl(repository.GetById(id));
        }
    }
}