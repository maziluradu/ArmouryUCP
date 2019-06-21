using System;

namespace ArmouryUCP.WebAPI.Models
{
    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int Skin { get; set; }

        public string DateNice
        {
            get
            {
                return Date == DateTime.Today ? "Today" : ((DateTime.Today - Date).TotalDays == -1 ? "Tomorrow" : ((DateTime.Today - Date).TotalDays == 1) ? "Yesterday" : Date.ToString("d"));
            }
        }
    }
}