namespace ArmouryUCP.WebAPI.Models
{
    public class PlayerComplaintInformation
    {
        public int ComplaintsCreated { get; set; }
        public int ComplaintsAgainst { get; set; }
        public int SuccessfulComplaints { get; set; }
        public int SanctionsReceived { get; set; }
    }
}