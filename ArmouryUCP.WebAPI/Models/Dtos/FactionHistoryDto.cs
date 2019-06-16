using System;

namespace ArmouryUCP.WebAPI.Models.Dtos
{
    public class FactionHistoryDto
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public string DateNice
        {
            get
            {
                return Date == DateTime.Today ? "Today" : ((DateTime.Today - Date).TotalDays == -1 ? "Tomorrow" : ((DateTime.Today - Date).TotalDays == 1) ? "Yesterday" : Date.ToString("d"));
            }
        }
    }
}