using ArmouryUCP.WebAPI.Models;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IVehicleService
    {
        List<Vehicle> GetVehicles(string owner);
    }
}
