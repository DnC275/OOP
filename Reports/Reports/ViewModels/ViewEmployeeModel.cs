using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Reports
{
    public class ViewEmployeeModel
    {
        private int id;
        private string name;
        private int directorId;
        private List<int> subordinatesId;

        public int Id => id;
        
        public string Name => name;

        public int DirectorId => directorId;

        public List<int> GetSubordinatesId()
        {
            return new List<int>(subordinatesId);
        }

        public ViewEmployeeModel(int id, string name, int directorId, params int[] p)
        {
            this.id = id;
            this.name = name;
            this.directorId = directorId;
            subordinatesId = new List<int>(p);
        }
        
        public ViewEmployeeModel(string name, int directorId, params int[] p)
        {
            this.name = name;
            this.directorId = directorId;
            subordinatesId = new List<int>(p);
        }

        public string Show(string prefix)
        {
            StringBuilder str = new StringBuilder(prefix).Append(Id.ToString()).Append('(').Append(Name).Append(')');
            Console.WriteLine(str.ToString());
            return str.Append(" -> ").ToString();
        }
    }
}