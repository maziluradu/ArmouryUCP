using ArmouryUCP.WebAPI.Models;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IBusinessService
    {
        List<Business> GetBusinesses(string owner);
    }
}
