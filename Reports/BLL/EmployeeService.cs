using System;
using System.Collections.Generic;
using System.Reflection;
using BLL.DTO;
using BLL.Infrastructure;
using DAL;
using DAL.Entities;
using DAL.Infrastructure;
using Exceptions;

namespace BLL
{
    public class EmployeeService : IEmployeeService
    {
        private IRepository<Employee> repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            this.repository = repository;
        }
        
        public int Add(EmployeeDTO entity)
        {
            if (entity.DirectorId != -1 && !repository.CheckIdExistence(entity.DirectorId))
                throw new NonexistentDirector(); 
            Employee em = ConvertBblToDal(entity);
            int res_id = repository.Create(ConvertBblToDal(entity));
            if (em.DirectorId != -1)
            {
                repository.Update(ConvertBblToDal(ConvertDalToBbl(repository.GetById(em.DirectorId)).AddSubordinate(res_id)));
            }
            return res_id;
        }

        public Dictionary<int, EmployeeDTO> GetAll()
        {
            Dictionary<int, Employee> dict = new Dictionary<int, Employee>(repository.GetAll());
            Dictionary<int, EmployeeDTO> res_dict = new Dictionary<int, EmployeeDTO>();
            foreach (var id in dict.Keys)
            {
                res_dict.Add(id, ConvertDalToBbl(dict[id]));
            }
            return res_dict;
        }

        public void GetSubordinatesId(int directorId, List<int> res_list)
        {
            foreach (var subordinateId in repository.GetById(directorId).GetSubordinatesId())
            {
                res_list.Add(subordinateId);
                GetSubordinatesId(subordinateId, res_list);
            }
        }

        public void ChangeDirector(int employeeId, int newDirectorId)
        {
            if (!CheckExistence(employeeId) || !CheckExistence(newDirectorId))
                throw new NonexistentEmployee();
            if (CheckOnTeamlead(employeeId))
                throw new ChangeDirectorOfLeader();
            EmployeeDTO employeeDto = ConvertDalToBbl(repository.GetById(employeeId));
            EmployeeDTO newDirectorDto = ConvertDalToBbl(repository.GetById(newDirectorId));
            EmployeeDTO prevDirectorDto = ConvertDalToBbl(repository.GetById(employeeDto.DirectorId));
            List<int> subordinatesId = new List<int>();
            GetSubordinatesId(employeeId, subordinatesId);
            if (subordinatesId.Contains(newDirectorId))
            {
                EmployeeDTO prevDirectorOfNewDirector = ConvertDalToBbl(repository.GetById(newDirectorDto.DirectorId));
                prevDirectorOfNewDirector.RemoveSubordinate(newDirectorId);
                employeeDto.DirectorId = newDirectorId;
                newDirectorDto.AddSubordinate(employeeId);
                newDirectorDto.DirectorId = prevDirectorDto.Id;
                prevDirectorDto.RemoveSubordinate(employeeId);
                prevDirectorDto.AddSubordinate(newDirectorId);
                repository.Update(ConvertBblToDal(prevDirectorOfNewDirector));
            }
            else
            {
                prevDirectorDto.RemoveSubordinate(employeeId);
                newDirectorDto.AddSubordinate(employeeId);
                employeeDto.DirectorId = newDirectorId;
            }
            repository.Update(ConvertBblToDal(employeeDto));
            repository.Update(ConvertBblToDal(newDirectorDto));
            repository.Update(ConvertBblToDal(prevDirectorDto));
        }
        
        

        public bool CheckOnTeamlead(int employeeId)
        {
            Dictionary<int, Employee> dict = repository.GetAll();
            foreach (var employee in dict.Values)
            {
                if (employee.Id == employeeId)
                {
                    return employee.DirectorId == -1;
                }
            }
            return false;
        }

        public Employee ConvertBblToDal(EmployeeDTO entity)
        {
            return new Employee(entity.Id, entity.Name, entity.DirectorId, entity.GetSubordinatesId().ToArray());
        }
        
        public EmployeeDTO ConvertDalToBbl(Employee entity)
        {
            return new EmployeeDTO(entity.Id, entity.Name, entity.DirectorId, entity.GetSubordinatesId().ToArray());
        }

        public bool CheckExistence(int id)
        {
            return repository.CheckIdExistence(id);
        }

        public EmployeeDTO Get(int id)
        {
            return ConvertDalToBbl(repository.GetById(id));
        }
    }
}