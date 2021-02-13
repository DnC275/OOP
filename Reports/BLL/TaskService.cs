using System;
using System.Collections.Generic;
using System.Reflection;
using BLL.DTO;
using BLL.Infrastructure;
using DAL;
using DAL.Entities;
using DAL.Infrastructure;
using Data;
using Exceptions;

namespace BLL
{
    public class TaskService : ITaskService
    {
        private IRepository<Task> repository;
        private IRepository<TaskChange> changeRepository;
        private IEmployeeService employeeService;

        public TaskService(IRepository<Task> repository, IRepository<TaskChange> changeRepository, IEmployeeService employeeService)
        {
            this.repository = repository;
            this.changeRepository = changeRepository;
            this.employeeService = employeeService;
        }
        
        public int Add(TaskDTO entity)
        {
            return repository.Create(ConvertBblToDal(entity));
        }

        public TaskDTO Get(int id)
        {
            if (!repository.CheckIdExistence(id))
                throw new NonexistentTask();
            return ConvertDalToBbl(repository.GetById(id));
        }

        public void AssignEmployee(int taskId, int employeeId)
        {
            if (!employeeService.CheckExistence(employeeId))
                throw new NonexistentEmployee();
            TaskDTO taskDto = ConvertDalToBbl(repository.GetById(taskId));
            taskDto.EmployeeId = employeeId;
            repository.Update(ConvertBblToDal(taskDto));
            changeRepository.Create(ConvertBblToDal(new TaskChangeDTO(taskId, employeeId,
                "An employee has been assigned to perform this task")));
        }

        public void AddComment(int taskId, int employeeId, string text)
        {
            if (!repository.CheckIdExistence(taskId))
                throw new NonexistentTask();
            TaskDTO taskDto = ConvertDalToBbl(repository.GetById(taskId));
            taskDto.AddComment(text);
            taskDto.LastModifiedDate = MyDate.GetDate();
            repository.Update(ConvertBblToDal(taskDto));
            changeRepository.Create(ConvertBblToDal(new TaskChangeDTO(taskId, employeeId, "New comment added")));
        }

        public void MakeAction(int taskId, int employeeId)
        {
            if (!repository.CheckIdExistence(taskId))
                throw new NonexistentTask();
            TaskDTO taskDto = ConvertDalToBbl(repository.GetById(taskId));
            if (employeeId != taskDto.EmployeeId)
                throw new TaskAssignedToAnother();
            if (taskDto.Status == Task.TaskStatus.Resolved)
                throw new TaskAlreadyCompleted();
            if (taskDto.Status == Task.TaskStatus.Open)
            {
                taskDto.Status = Task.TaskStatus.Active;
                repository.Update(ConvertBblToDal(taskDto));
            }
            changeRepository.Create(
                ConvertBblToDal(new TaskChangeDTO(taskId, employeeId, "An action was performed on this task")));
        }

        public void CompleteTask(int taskId, int employeeId)
        {
            if (!repository.CheckIdExistence(taskId))
                throw new NonexistentTask();
            TaskDTO taskDto = ConvertDalToBbl(repository.GetById(taskId));
            if (employeeId != taskDto.EmployeeId)
                throw new TaskAssignedToAnother();
            if (taskDto.Status == Task.TaskStatus.Resolved)
                throw new TaskAlreadyCompleted();
            taskDto.Status = Task.TaskStatus.Resolved;
            repository.Update(ConvertBblToDal(taskDto));
            changeRepository.Create(ConvertBblToDal(new TaskChangeDTO(taskId, employeeId, "Task solved")));
        }

        public Dictionary<int, TaskDTO> GetAll()
        {
            Dictionary<int, Task> dict = repository.GetAll();
            Dictionary<int, TaskDTO> res_dict = new Dictionary<int, TaskDTO>();
            foreach (var id in dict.Keys)
            {
                res_dict.Add(id, ConvertDalToBbl(dict[id]));
            }
            return res_dict;
        }

        public List<TaskDTO> GetTasksByCreationDate(DateTime date)
        {
            List <TaskDTO> res_list = new List<TaskDTO>();
            Dictionary<int, Task> dict = repository.GetAll();
            foreach (var task in dict.Values)
            {
                if (task.CreationDate.Day == date.Day && task.CreationDate.Month == date.Month &&
                    task.CreationDate.Year == date.Year)
                {
                    res_list.Add(ConvertDalToBbl(task));
                }
            }
            return res_list;
        }
        
        public List<TaskDTO> GetTasksByLastModifiedDate(DateTime date)
        {
            List <TaskDTO> res_list = new List<TaskDTO>();
            Dictionary<int, Task> dict = repository.GetAll();
            foreach (var task in dict.Values)
            {
                if (task.LastModifiedDate.Day == date.Day && task.LastModifiedDate.Month == date.Month &&
                    task.LastModifiedDate.Year == date.Year)
                {
                    res_list.Add(ConvertDalToBbl(task));
                }
            }
            return res_list;
        }
        
        public List<TaskDTO> GetTasksByEmployee(int employeeId)
        {
            List <TaskDTO> res_list = new List<TaskDTO>();
            Dictionary<int, Task> dict = repository.GetAll();
            foreach (var task in dict.Values)
            {
                if (task.EmployeeId == employeeId)
                {
                    res_list.Add(ConvertDalToBbl(task));
                }
            }
            return res_list;
        }

        public List<TaskDTO> GetTasksModifiedByEmployee(int employeeId)
        {
            List<TaskDTO> res_list = new List<TaskDTO>();
            HashSet<int> checkSet = new HashSet<int>();
            Dictionary<int, TaskChange> dict = changeRepository.GetAll();
            foreach (var taskChange in dict.Values)
            {
                if (taskChange.EmployeeId == employeeId)
                {
                    if (!checkSet.Contains(taskChange.TaskId))
                    {
                        checkSet.Add(taskChange.TaskId);
                        res_list.Add(ConvertDalToBbl(repository.GetById(taskChange.TaskId)));
                    }
                }
            }
            return res_list;
        }

        public List<TaskDTO> GetSubordinatesTasks(int directorId)
        {
            List<int> subordinatesId = new List<int>();
            employeeService.GetSubordinatesId(directorId, subordinatesId);
            Dictionary<int, Task> dict = repository.GetAll();
            List<TaskDTO> res_list = new List<TaskDTO>();
            foreach (var task in dict.Values)
            {
                if (subordinatesId.Contains(task.EmployeeId))
                {
                    res_list.Add(ConvertDalToBbl(task));
                }
            }
            return res_list;
        }

        public TaskDTO ConvertDalToBbl(Task entity)
        {
            return new TaskDTO(entity.Id, entity.Name, entity.Description, entity.Status, entity.CreationDate, entity.LastModifiedDate, entity.EmployeeId, 
                entity.GetComments().ToArray());
        }

        public Task ConvertBblToDal(TaskDTO entity)
        {
            return new Task(entity.Id, entity.Name, entity.Description, entity.Status, entity.CreationDate, entity.LastModifiedDate, entity.EmployeeId, 
                entity.GetComments().ToArray());
        }
        
        public TaskChangeDTO ConvertDalToBbl(TaskChange entity)
        {
            return new TaskChangeDTO(entity.Id, entity.TaskId, entity.EmployeeId, entity.ChangeText, entity.ChangeDate);
        }

        public TaskChange ConvertBblToDal(TaskChangeDTO entity)
        {
            return new TaskChange(entity.Id, entity.TaskId, entity.EmployeeId, entity.ChangeText, entity.ChangeDate);
        }

        public bool CheckExistence(int id)
        {
            return repository.CheckIdExistence(id);
        }
    }
}