using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Infrastructure;
using Data;

namespace DAL
{
    public class TaskChangeRepository : IRepository<TaskChange>
    {
        private Dictionary<int, TaskChange> changes;

        public TaskChangeRepository()
        {
            changes = new Dictionary<int, TaskChange>();
        }
        
        public Dictionary<int, TaskChange> GetAll()
        {
            return new Dictionary<int, TaskChange>(changes);
        }

        public TaskChange GetById(int id)
        {
            return null;
        }

        public int Create(TaskChange entity)
        {
            entity.Id = changes.Keys.Count == 0 ? 1 : changes.Keys.Max() + 1;
            entity.ChangeDate = MyDate.GetDate();
            changes.Add(entity.Id, entity);
            return entity.Id;
        }

        public void Update(TaskChange entity)
        {
        }

        public void Delete(TaskChange entity)
        {
            
        }

        public bool CheckIdExistence(int id)
        {
            return changes.ContainsKey(id);
        }
    }
}