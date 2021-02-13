using System.Collections.Generic;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Infrastructure
{
    public interface IEmployeeService : IService<EmployeeDTO, Employee>
    {
        public void GetSubordinatesId(int directorId, List<int> res_list);

        public void ChangeDirector(int employeeId, int newDirectorId);
    }
}