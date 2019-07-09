using ArmouryUCP.WebAPI.Models;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IHouseService
    {
        List<House> GetHouses(string owner);
        List<House> GetHouses(int number = 0, int start = 0);
        House GetHouse(int id);

        House GetHouseWithTenants();
        GlobalHouseInformation GetGlobalInformationForHouses();
    }
}
