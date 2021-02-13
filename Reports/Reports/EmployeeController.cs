using System;
using System.Collections.Generic;
using BLL.DTO;
using BLL.Infrastructure;
using Reports.Infrastructure;

namespace Reports
{
    public class EmployeeController : IController<ViewEmployeeModel, EmployeeDTO>
    {
        private IEmployeeService service;

        public EmployeeController(IEmployeeService service)
        {
            this.service = service;
        }
        
        public int AddEmployee(string name, int directorId = -1)
        {
            ViewEmployeeModel employeeModel = new ViewEmployeeModel(name, directorId);
            return service.Add(ConvertModelToBbl(employeeModel));
        }

        public Dictionary<int, ViewEmployeeModel> GetAll()
        {
            Dictionary<int, EmployeeDTO> dict = new Dictionary<int, EmployeeDTO>(service.GetAll());
            Dictionary<int, ViewEmployeeModel> res_dict = new Dictionary<int, ViewEmployeeModel>();
            foreach (var id in dict.Keys)
            {
                res_dict.Add(id, ConvertBblToModel(dict[id]));
            }
            return res_dict;
        }
        
        public void ShowHierarchyOfEmployees()
        {
            Dictionary<int, ViewEmployeeModel> dict = GetAll();
            foreach (var em in dict.Values)
            {
                if (em.DirectorId == -1)
                {
                    ShowEmployee(dict, em.Id, "");
                    break;
                }
            }
        }

        private void ShowEmployee(Dictionary<int, ViewEmployeeModel> dict, int employeeModelId, string prefix)
        {
            prefix = dict[employeeModelId].Show(prefix);
            foreach (var id in dict[employeeModelId].GetSubordinatesId())
            {
                ShowEmployee(dict, id, prefix);
            }
        }

        public void ChangeDirector(int employeeId, int newDirectorId)
        {
            service.ChangeDirector(employeeId, newDirectorId);
        }
        
        public EmployeeDTO ConvertModelToBbl(ViewEmployeeModel entity)
        {
            return new EmployeeDTO(entity.Name, entity.DirectorId);
        }

        public ViewEmployeeModel ConvertBblToModel(EmployeeDTO entity)
        {
            return new ViewEmployeeModel(entity.Id, entity.Name, entity.DirectorId, entity.GetSubordinatesId().ToArray());
        }
    }
}