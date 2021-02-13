using System.Collections.Generic;
using BLL;
using BLL.DTO;
using BLL.Infrastructure;
using DAL.Entities;
using Reports.Infrastructure;

namespace Reports
{
    public class ReportController : IController<ViewReportModel, ReportDTO>
    {
        private IReportService service;

        public ReportController(IReportService service)
        {
            this.service = service;
        }
        
        public int CreateReport(int employeeId, string text)
        {
            ViewReportModel reportModel = new ViewReportModel(employeeId, text);
            return service.Add(ConvertModelToBbl(reportModel));
        }

        public void AddCompletedTask(int reportId, int taskId)
        {
            service.AddCompletedTask(reportId, taskId);
        }

        public void UpdateText(int reportId, string text)
        {
            service.UpdateText(reportId, text);
        }

        public ViewReportModel GetReport(int reportId)
        {
            return ConvertBblToModel(service.Get(reportId));
        }

        public List<ViewReportModel> GetReportsByEmployee(int employeeId)
        {
            List<ReportDTO> list = service.GetReportsByEmployee(employeeId);
            List<ViewReportModel> res_list = new List<ViewReportModel>();
            foreach (var reportDTO in list)
            {
                res_list.Add(ConvertBblToModel(reportDTO));
            }
            return res_list;
        }
        
        public ReportDTO ConvertModelToBbl(ViewReportModel entity)
        {
            return new ReportDTO(entity.Id, entity.EmployeeId, entity.CreationDate, entity.Text, entity.GetCompletedTasks().ToArray());
        }

        public ViewReportModel ConvertBblToModel(ReportDTO entity)
        {
            return new ViewReportModel(entity.Id, entity.EmployeeId, entity.CreationDate, entity.Text, entity.GetCompletedTasks().ToArray());
        }
    }
}