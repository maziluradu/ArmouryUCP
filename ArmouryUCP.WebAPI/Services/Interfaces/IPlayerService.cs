﻿using ArmouryUCP.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmouryUCP.WebAPI.Services.Interfaces
{
    public interface IPlayerService
    {
        List<Player> GetPlayers(int playersToReturn = 10);
        Player GetPlayer(int id);
        List<FactionHistory> GetFactionHistory(int id);
    }
}
