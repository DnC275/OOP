using System;
using System.Collections.Generic;
using BLL;
using BLL.DTO;
using BLL.Infrastructure;
using Reports.Infrastructure;

namespace Reports
{
    public class TaskController : IController<ViewTaskModel, TaskDTO>
    {
        private ITaskService service;

        public TaskController(TaskService service)
        {
            this.service = service;
        }

        public int AddTask(string name, string description)
        {
            return service.Add(ConvertModelToBbl(new ViewTaskModel(name, description)));
        }

        public ViewTaskModel GetTaskById(int id) => ConvertBblToModel(service.Get(id));

        public void ShowTaskById(int id) => ConvertBblToModel(service.Get(id)).Show();

        public List<ViewTaskModel> GetTasksByCreationDate(DateTime date)
        {
            List <TaskDTO> list = service.GetTasksByCreationDate(date);
            List <ViewTaskModel> res_list = new List<ViewTaskModel>();
            foreach (var taskDto in list)
            {
                res_list.Add(ConvertBblToModel(taskDto));
            }
            return res_list;
        }
        
        public List<ViewTaskModel> GetTasksByLastModifiedDate(DateTime date)
        {
            List <TaskDTO> list = service.GetTasksByLastModifiedDate(date);
            List <ViewTaskModel> res_list = new List<ViewTaskModel>();
            foreach (var taskDto in list)
            {
                res_list.Add(ConvertBblToModel(taskDto));
            }
            return res_list;
        }

        public List<ViewTaskModel> GetTasksByEmployee(int employeeId)
        {
            List <TaskDTO> list = service.GetTasksByEmployee(employeeId);
            List <ViewTaskModel> res_list = new List<ViewTaskModel>();
            foreach (var taskDto in list)
            {
                res_list.Add(ConvertBblToModel(taskDto));
            }
            return res_list;
        }

        public List<ViewTaskModel> GetTasksModifiedByEmployee(int employeeId)
        {
            List <TaskDTO> list = service.GetTasksModifiedByEmployee(employeeId);
            List <ViewTaskModel> res_list = new List<ViewTaskModel>();
            foreach (var taskDto in list)
            {
                res_list.Add(ConvertBblToModel(taskDto));
            }
            return res_list;
        }

        public List<ViewTaskModel> GetSubordinatesTasks(int directorId)
        {
            List <TaskDTO> list = service.GetSubordinatesTasks(directorId);
            List <ViewTaskModel> res_list = new List<ViewTaskModel>();
            foreach (var taskDto in list)
            {
                res_list.Add(ConvertBblToModel(taskDto));
            }
            return res_list;
        }
        
        public void AssignEmployee(int taskId, int employeeId)
        {
            service.AssignEmployee(taskId, employeeId);
        }

        public void AddComment(int taskId, int employeeId, string text) => service.AddComment(taskId, employeeId, text);

        public void MakeAction(int taskId, int employeeId) => service.MakeAction(taskId, employeeId);

        public void CompleteTask(int taskId, int employeeId) => service.CompleteTask(taskId, employeeId);
        
        public TaskDTO ConvertModelToBbl(ViewTaskModel entity)
        {
            return new TaskDTO(entity.Id, entity.Name, entity.Description, entity.Status, entity.CreationDate, entity.LastModifiedDate, entity.EmployeeId, 
                entity.GetComments().ToArray());
        }

        public ViewTaskModel ConvertBblToModel(TaskDTO entity)
        {
            return new ViewTaskModel(entity.Id, entity.Name, entity.Description, entity.Status, entity.CreationDate, entity.LastModifiedDate, entity.EmployeeId, 
                entity.GetComments().ToArray());
        }
    }
}