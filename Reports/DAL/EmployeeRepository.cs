using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using DAL.Entities;
using DAL.Infrastructure;

namespace DAL
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private Dictionary<int, Employee> employees;

        public EmployeeRepository()
        {
            employees = new Dictionary<int, Employee>();
        }

        public Dictionary<int, Employee> GetAll()
        {
            return new Dictionary<int, Employee>(employees);
        }

        public Employee GetById(int id)
        {
            return employees[id];
        }

        public int Create(Employee entity)
        {
            entity.Id = employees.Keys.Count == 0 ? 1 : employees.Keys.Max() + 1;
            employees.Add(entity.Id, entity);
            return entity.Id;
        }

        public void Update(Employee entity)
        {
            employees[entity.Id] = entity;
        }

        public void Delete(Employee entity)
        {
            employees.Remove(entity.Id);
        }

        public bool CheckIdExistence(int id)
        {
            return employees.ContainsKey(id);
        }
    }
}