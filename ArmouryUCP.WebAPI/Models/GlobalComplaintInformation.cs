namespace ArmouryUCP.WebAPI.Models
{
    public class GlobalComplaintInformation
    {
        public int Total { get; set; }
        public int TotalOpen { get; set; }
        public PlayerComplaintInformation PlayerComplaintInformation { get; set; }

        public int TotalClosed
        {
            get
            {
                return Total - TotalOpen;
            }
        }

        public GlobalComplaintInformation()
        {

        }
    }
}