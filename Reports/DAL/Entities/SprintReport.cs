using System;
using System.Collections.Generic;
using DAL.Infrastructure;

namespace DAL.Entities
{
    public class SprintReport : IEntity
    {
        private int id;
        private string text;
        private int directorId;
        private DateTime closingDate;
        private DateTime creationDate;
        private List<int> dailyReportsId;
        
        public int Id { 
            get => id;
            internal set => id = value;
        }

        public string Text => text;

        public int DirectorId => directorId;

        public DateTime ClosingDate
        {
            get => closingDate;
            internal set => closingDate = value;
        }

        public DateTime CreationDate
        {
            get => creationDate;
            internal set => creationDate = value;
        }

        public List<int> GetReports()
        {
            return new List<int>(dailyReportsId);
        }

        public SprintReport(int id, int directorId, DateTime creationDate, DateTime closingDate, string text, List<int> reportsId)
        {
            this.id = id;
            this.directorId = directorId;
            this.creationDate = creationDate;
            this.closingDate = closingDate;
            this.text = text;
            dailyReportsId = new List<int>(reportsId);
        }
    }
}