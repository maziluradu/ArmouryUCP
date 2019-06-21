using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArmouryUCP.WebAPI.Models.Dtos
{
    public class PlayerShortDto
    {
        public int ID { get; set; }
        public int Model { get; set; }
        public string Name { get; set; }
        public int TotalPlayers { get; set; }
    }
}