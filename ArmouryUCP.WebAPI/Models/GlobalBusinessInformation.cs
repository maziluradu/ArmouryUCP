namespace ArmouryUCP.WebAPI.Models
{
    public class GlobalBusinessInformation
    {
        public int Total { get; set; }
        public int TotalOwned { get; set; }
        public int TotalForSale { get; set; }
        public int TotalTenants { get; set; }

        public GlobalBusinessInformation()
        {

        }
    }
}