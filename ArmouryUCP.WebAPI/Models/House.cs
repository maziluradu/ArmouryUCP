using System;

namespace ArmouryUCP.WebAPI.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Value { get; set; }
        public int Price { get; set; }
        public int InteriorIndex { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string Locked { get; set; }
        public bool Rentable { get; set; }
        public int RentPrice { get; set; }
        public int FurniturePieces { get; set; }
        public bool HealthUpgrade { get; set; }
        public bool ArmourUpgrade { get; set; }
        public int GarageSlots { get; set; }
        public int TenantsNumber { get; set; }
    }
}