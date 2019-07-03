using ArmouryUCP.WebAPI.Models;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IVehicleService
    {
        List<Vehicle> GetVehicles(string owner);
        List<Vehicle> GetVehicles(int number = 0, int start = 0);
        Vehicle GetVehicle(int id);
        GlobalVehicleInformation GetGlobalInformationForVehicles();
    }
}
