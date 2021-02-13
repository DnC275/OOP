using System;
using BLL;
using BLL.DTO;
using BLL.Infrastructure;
using Reports.Infrastructure;

namespace Reports
{
    public class SprintReportController : IController<ViewSprintReportModel, SprintReportDTO>
    {
        private ISprintService service;

        public SprintReportController(SprintReportService service)
        {
            this.service = service;
        }

        public int CreateSprintReport(int directorId, DateTime closingDate, string text = "")
        {
            return service.Add(ConvertModelToBbl(new ViewSprintReportModel(directorId, closingDate, text)));
        }

        public void AddDailyReport(int sprintId, int employeeId, int reportId)
        {
            service.AddDailyReport(sprintId, employeeId, reportId);
        }
        
        public SprintReportDTO ConvertModelToBbl(ViewSprintReportModel entity)
        {
            return new SprintReportDTO(entity.Id, entity.DirectorId, entity.CreationDate, entity.ClosingDate, entity.Text, entity.GetReports());
        }

        public ViewSprintReportModel ConvertBblToModel(SprintReportDTO entity)
        {
            return new ViewSprintReportModel(entity.Id, entity.DirectorId, entity.CreationDate, entity.ClosingDate, entity.Text, entity.GetReports());
        }
    }
}