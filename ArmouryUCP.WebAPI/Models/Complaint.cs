using System;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Models
{
    public class Complaint
    {
        public int ID { get; set; }
        public int ReporterID { get; set; }
        public int ReportedID { get; set; }
        public int ReporterSkin { get; set; }
        public int ReportedSkin { get; set; }
        public string ReporterName { get; set; }
        public string ReportedName { get; set; }
        public int ReporterLevelProgress { get; set; }
        public int ReportedLevelProgress { get; set; }
        public int ReporterLevel { get; set; }
        public int ReportedLevel { get; set; }
        public string ReporterFaction { get; set; }
        public string ReportedFaction { get; set; }
        public DateTime CreationDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public List<Comment> Comments { get; set; }
    }
}