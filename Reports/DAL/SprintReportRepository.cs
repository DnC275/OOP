using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Infrastructure;
using Data;

namespace DAL
{
    public class SprintReportRepository : IRepository<SprintReport>
    {
        private Dictionary<int, SprintReport> sprintReports;

        public SprintReportRepository()
        {
            sprintReports = new Dictionary<int, SprintReport>();
        }
        
        public Dictionary<int, SprintReport> GetAll()
        {
            return new Dictionary<int, SprintReport>(sprintReports);
        }
        
        public SprintReport GetById(int id)
        {
            return sprintReports[id];
        }

        public int Create(SprintReport entity)
        {
            entity.Id = sprintReports.Keys.Count == 0 ? 1 : sprintReports.Keys.Max() + 1;
            entity.CreationDate = MyDate.GetDate();
            sprintReports.Add(entity.Id, entity);
            return entity.Id;
        }

        public void Update(SprintReport entity)
        {
            sprintReports[entity.Id] = entity;
        }

        public void Delete(SprintReport entity)
        {
            sprintReports.Remove(entity.Id);
        }

        public bool CheckIdExistence(int id)
        {
            return sprintReports.Keys.Contains(id);
        }
    }
}