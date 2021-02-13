using System;
using System.Collections.Generic;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Infrastructure
{
    public interface ITaskService : IService<TaskDTO, Task>
    {
        public void AssignEmployee(int taskId, int employeeId);

        public void AddComment(int taskId, int employeeId, string text);

        public void MakeAction(int taskId, int employeeId);
        
        public void CompleteTask(int taskId, int employeeId);

        public List<TaskDTO> GetTasksByCreationDate(DateTime date);

        public List<TaskDTO> GetTasksByLastModifiedDate(DateTime date);

        public List<TaskDTO> GetTasksByEmployee(int employeeId);

        public List<TaskDTO> GetTasksModifiedByEmployee(int employeeId);

        public List<TaskDTO> GetSubordinatesTasks(int directorId);
    }
}