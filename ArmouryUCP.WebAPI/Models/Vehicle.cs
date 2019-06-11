using System;

namespace ArmouryUCP.WebAPI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public int Model { get; set; }
        public int Value { get; set; }
    }
}