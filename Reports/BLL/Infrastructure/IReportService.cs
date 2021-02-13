using System.Collections.Generic;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Infrastructure
{
    public interface IReportService : IService<ReportDTO, Report>
    {
        public void AddCompletedTask(int reportId, int taskId);

        public void UpdateText(int reportId, string text);

        public List<ReportDTO> GetReportsByEmployee(int employeeId);

        public bool CheckForCompleteness(int reportId);
    }
}