using BLL.DTO;
using DAL.Entities;

namespace BLL.Infrastructure
{
    public interface ISprintService : IService<SprintReportDTO, SprintReport>
    {
        public void AddDailyReport(int sprintId, int employeeId, int reportId);
    }
}