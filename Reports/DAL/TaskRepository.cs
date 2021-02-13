using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using DAL.Entities;
using DAL.Infrastructure;
using Data;

namespace DAL
{
    public class TaskRepository : IRepository<Task>
    {
        private Dictionary<int, Task> tasks;

        public TaskRepository()
        {
            tasks = new Dictionary<int, Task>();
        }
        
        public Dictionary<int, Task> GetAll()
        {
            return new Dictionary<int, Task>(tasks);
        }

        public Task GetById(int id)
        {
            return tasks[id];
        }

        public int Create(Task entity)
        {
            entity.Id = tasks.Keys.Count == 0 ? 1 : tasks.Keys.Max() + 1;
            entity.CreationDate = MyDate.GetDate();
            entity.LastModifiedDate = entity.CreationDate;
            entity.Status = Task.TaskStatus.Open;
            tasks.Add(entity.Id, entity);
            return entity.Id;
        }

        public void Update(Task entity)
        {
            tasks[entity.Id] = entity;
        }

        public void Delete(Task entity)
        {
            tasks.Remove(entity.Id);
        }

        public bool CheckIdExistence(int id)
        {
            return tasks.ContainsKey(id);
        }
    }
}