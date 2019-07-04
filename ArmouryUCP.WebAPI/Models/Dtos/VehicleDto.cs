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
        public int KM { get; set; }
        public int Color1 { get; set; }
        public int Color2 { get; set; }
        public string Plate { get; set; }
        public int Premium { get; set; }
        public DateTime DateOfPurchase { get; set; }

        public string DateOfPurchaseNice
        {
            get
            {
                return DateOfPurchase.ToString(DateOfPurchase.Year < DateTime.Now.Year ? "dddd, dd MMMM \"'\"yy" : "dddd, dd MMMM");
            }
        }

        public string Name
        {
            get
            {
                return SharedResources.VehicleNames.ElementAtOrDefault(Model - 400);
            }
        }
    }
}