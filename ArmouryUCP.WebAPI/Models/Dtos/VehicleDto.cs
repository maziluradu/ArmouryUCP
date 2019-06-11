using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ArmouryUCP.WebAPI.Models.Dtos
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public int Model { get; set; }
        public int Value { get; set; }

        public string Name
        {
            get
            {
                return SharedResources.VehicleNames[Model-400];
            }
        }
    }
}