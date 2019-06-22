namespace ArmouryUCP.WebAPI.Models
{
    public class GlobalHouseInformation
    {
        public int Total { get; set; }
        public int TotalOwned { get; set; }
        public int TotalForSale { get; set; }
        public int TotalTenants { get; set; }

        public int TotalPages
        {
            get
            {
                return Total/10;
            }
        }


        public GlobalHouseInformation()
        {

        }
    }
}