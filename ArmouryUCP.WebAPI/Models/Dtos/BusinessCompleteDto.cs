using System;
using System.Text.RegularExpressions;

namespace ArmouryUCP.WebAPI.Models.Dtos
{
    public class BusinessCompleteDto
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

        public string DateOfPurchaseNice
        {
            get
            {
                return DateOfPurchase.ToString(DateOfPurchase.Year < DateTime.Now.Year ? "dddd, dd MMMM \"'\"yy" : "dddd, dd MMMM");
            }
        }
    }
}