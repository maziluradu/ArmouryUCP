using System;
using System.Text.RegularExpressions;

namespace ArmouryUCP.WebAPI.Models.Dtos
{
    public class HouseCompleteDto
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Value { get; set; }
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

        public string DateOfPurchaseNice
        {
            get
            {
                return DateOfPurchase.ToString(DateOfPurchase.Year < DateTime.Now.Year ? "dddd, dd MMMM \"'\"yy" : "dddd, dd MMMM");
            }
        }

        public int InteriorIndexNice
        {
            get
            {
                int ivInt;
                switch (InteriorIndex)
                {
                    case 1:
                        ivInt = 1;
                        break;
                    case 6:
                        ivInt = 2;
                        break;
                    case 14:
                        ivInt = 3;
                        break;
                    case 15:
                        ivInt = 4;
                        break;
                    case 16:
                        ivInt = 5;
                        break;
                    case 9:
                        ivInt = 6;
                        break;
                    case 2:
                        ivInt = 7;
                        break;
                    case 7:
                        ivInt = 8;
                        break;
                    case 8:
                        ivInt = 9;
                        break;
                    case 10:
                        ivInt = 10;
                        break;
                    case 11:
                        ivInt = 11;
                        break;
                    case 12:
                        ivInt = 12;
                        break;
                    case 19:
                        ivInt = 13;
                        break;
                    case 21:
                        ivInt = 14;
                        break;
                    case 24:
                        ivInt = 15;
                        break;
                    case 26:
                        ivInt = 16;
                        break;
                    case 27:
                        ivInt = 17;
                        break;
                    case 3:
                        ivInt = 18;
                        break;
                    case 5:
                        ivInt = 19;
                        break;
                    case 17:
                        ivInt = 21;
                        break;
                    case 18:
                        ivInt = 22;
                        break;
                    case 20:
                        ivInt = 23;
                        break;
                    case 22:
                        ivInt = 24;
                        break;
                    case 23:
                        ivInt = 25;
                        break;
                    case 25:
                        ivInt = 26;
                        break;
                    case 4:
                        ivInt = 27;
                        break;
                    case 13:
                        ivInt = 28;
                        break;
                    default:
                        ivInt = 1;
                        break;
                }

                return ivInt;
            }
        }

        public int ImagesNumber
        {
            get
            {
                switch(InteriorIndexNice)
                {
                    case 1:
                    case 14:
                    case 15:
                        return 1;
                    case 16:
                        return 2;
                    case 5:
                    case 12:
                    case 13:
                    case 21:
                        return 3;
                    case 2:
                    case 3:
                    case 4:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                        return 4;
                }
                return 1;
            }
        }

        public string NameNice
        {
            get
            {
                return Regex.Replace(Name, "{.*?}", string.Empty);
            }
        }

        public bool Furnished
        {
            get
            {
                return FurniturePieces > 0;
            }
        }

        public bool HasGarage
        {
            get
            {
                return GarageSlots > 0;
            }
        }
    }
}