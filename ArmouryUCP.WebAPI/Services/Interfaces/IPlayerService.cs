using ArmouryUCP.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IPlayerService
    {
        Player LoginPlayer(string username, string password);
        List<Player> GetPlayers(int playersToReturn = 10);
        List<Player> SearchPlayers(string name, bool incomplete = false);
        Player GetPlayer(int id);
        Player GetPlayer(string name);
        List<Player> GetOnlinePlayers(bool showFull = false);
        List<FactionHistory> GetFactionHistory(int id);
    }
}
