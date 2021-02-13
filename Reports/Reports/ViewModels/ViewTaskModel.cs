using System;
using System.Collections.Generic;
using System.IO;
using DAL.Entities;
using Data;

namespace Reports
{
    public class ViewTaskModel
    {
        private int id;
        private string name;
        private string description;
        private List<string> comments;
        private int employeeId;
        private Task.TaskStatus status;
        private DateTime creationDate;
        private DateTime lastModifiedDate;

        public int Id { get => id; internal set => id = value; }
        
        public string Name { get => name; internal set => name = value; }

        public DateTime CreationDate => creationDate;

        public DateTime LastModifiedDate => lastModifiedDate;

        public string Description { get => description; internal set => description = value; }

        public int EmployeeId { get => employeeId; internal set => employeeId = value; }

        public Task.TaskStatus Status { get => status; internal set => status = value; }
        
        public List<string> GetComments() => new List<string>(comments);

        public ViewTaskModel(int id, string name, string description, Task.TaskStatus status, DateTime creationDate, DateTime lastModifiedDate, int employeeId, params string[] comments)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.employeeId = employeeId;
            this.status = status;
            this.comments = new List<string>(comments);
            this.creationDate = creationDate;
            this.lastModifiedDate = lastModifiedDate;
        }

        public ViewTaskModel(string name, string description)
        {
            this.name = name;
            this.description = description;
            comments = new List<string>();
        }

        public void Show()
        {
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Description: {description}");
            Console.Write("Comments: ");
            foreach (var comment in comments)
            {
                Console.WriteLine($"----comment");
            }
            Console.WriteLine($"Responsible employee: {employeeId}");
            Console.WriteLine($"Creation date: {creationDate.Day} {creationDate.Month} {creationDate.Year}");
            Console.WriteLine($"Last modified date: {lastModifiedDate.Day} {lastModifiedDate.Month} {lastModifiedDate.Year}");
        }
    }
}