﻿using System;

namespace ArmouryUCP.WebAPI.Models
{
    public class Business
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Value { get; set; }
        public int Type { get; set; }
        public string Extortion { get; set; }
        public int Prods { get; set; }
        public int ComercialAd { get; set; }

        public DateTime DateOfPurchase { get; set; }
    }
}