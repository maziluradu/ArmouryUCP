using ArmouryUCP.WebAPI.Models;
using System.Collections.Generic;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IServerService
    {
        ServerInformation GetServerInformation();
        List<LogEntry> GetServerNews();
    }
}