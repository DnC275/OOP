using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Infrastructure;
using Data;

namespace DAL
{
    public class ReportRepository : IRepository<Report>
    {
        public Dictionary<int, Report> reports;

        public ReportRepository()
        {
            reports = new Dictionary<int, Report>();
        }
        
        public Dictionary<int, Report> GetAll()
        {
            return new Dictionary<int, Report>(reports);
        }

        public Report GetById(int id)
        {
            return reports[id];
        }

        public int Create(Report entity)
        {
            entity.Id = reports.Keys.Count == 0 ? 1 : reports.Keys.Max() + 1;
            entity.CreationDate = MyDate.GetDate();
            reports.Add(entity.Id, entity);
            return entity.Id;
        }

        public void Update(Report entity)
        {
            reports[entity.Id] = entity;
        }

        public void Delete(Report entity)
        {
        }

        public bool CheckIdExistence(int id)
        {
            return reports.ContainsKey(id);
        }
    }
}