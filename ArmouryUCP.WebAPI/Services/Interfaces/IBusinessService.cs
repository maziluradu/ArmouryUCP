using ArmouryUCP.WebAPI.Models;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IBusinessService
    {
        List<Business> GetBusinesses(string owner);
        List<Business> GetBusinesses(int number = 0, int start = 0);
        Business GetBusiness(int id);
        GlobalBusinessInformation GetGlobalInformationForBusinesses();
    }
}
