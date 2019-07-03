using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ArmouryUCP.WebAPI.Models.Dtos
{
    public class VehicleCompleteDto
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
        public string BrokenParts { get; set; }
        public DateTime DateOfPurchase { get; set; }

        public string DateOfPurchaseNice
        {
            get
            {
                return DateOfPurchase.ToString(DateOfPurchase.Year < DateTime.Now.Year ? "dddd, dd MMMM \"'\"yy" : "dddd, dd MMMM");
            }
        }

        public int BrokenEngine
        {
            get
            {
                return Convert.ToInt32(BrokenParts.Split('/')[0]);
            }
        }
        public int BrokenInjection
        {
            get
            {
                return Convert.ToInt32(BrokenParts.Split('/')[1]);
            }
        }
        public int BrokenBattery
        {
            get
            {
                return Convert.ToInt32(BrokenParts.Split('/')[2]);
            }
        }
        public int BrokenComputer
        {
            get
            {
                return Convert.ToInt32(BrokenParts.Split('/')[3]);
            }
        }
        public int BrokenDirection
        {
            get
            {
                return Convert.ToInt32(BrokenParts.Split('/')[4]);
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