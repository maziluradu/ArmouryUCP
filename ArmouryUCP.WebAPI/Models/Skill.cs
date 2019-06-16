using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArmouryUCP.WebAPI.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public string NameNice { get; set; }
        public int Progress { get; set; }
        public string Icon { get; set; }

        public int ProgressLevel
        {
            get
            {
                return Progress > 0 ? Progress / 100 : 0;
            }
        }
    }
}