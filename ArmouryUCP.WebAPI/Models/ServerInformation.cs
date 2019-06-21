using System;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Models
{
    public class ServerInformation
    {
        public long TotalPlayerMoney { get; set; }
        public int HighestLevel { get; set; }
        public int Uptime { get; set; }
        public string MostPopularJob { get; set; }
        public float MostPopularJobPercentage { get; set; }
        public long PreviousDayTotalMoney { get; set; }
        public string HighestEarner { get; set; }
        public int HighestEarnerId { get; set; }
        public long HighestEarnerMoney { get; set; }

        public int TotalMoneyModifyPercentage
        {
            get
            {
                return Convert.ToInt32(100 - (PreviousDayTotalMoney * 100 / TotalPlayerMoney));
            }
        }
    }
}