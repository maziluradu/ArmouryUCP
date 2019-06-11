using ArmouryUCP.WebAPI.Models;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IHouseService
    {
        List<House> GetHouses(string owner);
    }
}
