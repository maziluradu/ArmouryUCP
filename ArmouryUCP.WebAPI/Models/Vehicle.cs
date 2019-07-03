using System;

namespace ArmouryUCP.WebAPI.Models
{
    public class Vehicle
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
    }
}