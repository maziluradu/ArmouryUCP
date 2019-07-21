using ArmouryUCP.WebAPI.Models;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IComplaintService
    {
        Complaint GetComplaint(int complaint);
        List<Complaint> GetComplaints(int number = 0, int start = 0);
        GlobalComplaintInformation GetGlobalInformationForComplaints(int playerID = -1);
    }
}
