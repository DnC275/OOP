using System.Collections.Generic;
using System.IO;
using DAL.Infrastructure;

namespace DAL.Entities
{
    public class Employee : IEntity
    {
        private int id;
        private string name;
        private int directorId;
        private List<int> subordinatesId;

        public int Id { get => id; internal set => id = value; }

        public string Name { get => name; internal set => name = value; }

        public int DirectorId { get => directorId; internal set => directorId = value; }

        public List<int> GetSubordinatesId()
        {
            return new List<int>(subordinatesId);
        }

        public Employee(int id, string name, int directorId, params int[] p)
        {
            this.id = id;
            this.name = name;
            this.directorId = directorId;
            subordinatesId = new List<int>(p);
        }
    }
}



