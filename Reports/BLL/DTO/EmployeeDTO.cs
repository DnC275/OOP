using System.Collections.Generic;
using System.IO;

namespace BLL.DTO
{
    public class EmployeeDTO
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

        public EmployeeDTO(int id, string name, int directorId, params int[] p)
        {
            this.id = id;
            this.name = name;
            this.directorId = directorId;
            subordinatesId = new List<int>(p);
        }

        public EmployeeDTO(string name, int directorId, params int[] p) : this(-1, name, directorId, p)
        {
        }
        
        public EmployeeDTO AddSubordinate(int subordinateId)
        {
            subordinatesId.Add(subordinateId);
            return this;
        }

        public void RemoveSubordinate(int subordinateId)
        {
            subordinatesId.Remove(subordinateId);
        }
    }
}