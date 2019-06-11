using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ArmouryUCP.WebAPI.Models.Dtos
{
    public class HouseDto
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Value { get; set; }
        public int InteriorIndex { get; set; }
        public DateTime DateOfPurchase { get; set; }

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

        public string NameNice
        {
            get
            {
                return Regex.Replace(Name, "{.*?}", string.Empty);
            }
        }
    }
}