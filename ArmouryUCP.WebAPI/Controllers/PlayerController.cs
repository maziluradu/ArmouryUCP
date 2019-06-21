using ArmouryUCP.WebAPI.Models.Dtos;
using ArmouryUCP.WebAPI.Services.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace ArmouryUCP.WebAPI.Controllers
{
    [EnableCors(origins: "http://panel.armoury.ro", headers: "*", methods: "*")]
    public class PlayerController : ApiController
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        public PlayerController()
        {

        }

        /// <summary>
        /// Gets whole information about multiple players. Extremely inefficient. Only use this in
        /// critical cases - it outputs huge blocks of data.
        /// </summary>
        /// <returns>JSON containing information about multiple players</returns>
        [HttpGet]
        [Route("api/player")]
        public IHttpActionResult GetPlayers()
        {
            var players = Mapper.Map<List<PlayerDto>>(playerService.GetPlayers());
            return Ok(players);
        }

        /// <summary>
        /// Gets whole information about certain player. Only use this in critical cases - it outputs
        /// a lot of data.
        /// </summary>
        /// <param name="id">SQL ID of the user</param>
        /// <returns>JSON containing information about the player</returns>
        [HttpGet]
        [Route("api/player/{id}")]
        public IHttpActionResult GetPlayer(int id)
        {
            var player = Mapper.Map<PlayerDto>(playerService.GetPlayer(id));

            if (player != null)
                return Ok(player);

            return NotFound();
        }

        /// <summary>
        /// Gets faction history of a certain player
        /// </summary>
        /// <param name="id">SQL ID of the user</param>
        /// <returns>JSON containing the player's faction history</returns>
        [HttpGet]
        [Route("api/player/{id}/factionhistory")]
        public IHttpActionResult GetFactionHistory(int id)
        {
            var factionHistory = Mapper.Map<List<FactionHistoryDto>>(playerService.GetFactionHistory(id));

            if (factionHistory != null)
                return Ok(factionHistory);

            return NotFound();
        }

        /// <summary>
        /// Gets online players
        /// </summary>
        /// <returns>JSON containing few players data</returns>
        [HttpGet]
        [Route("api/player/briefonline")]
        public IHttpActionResult GetBriefOnlinePlayers()
        {
            var players = Mapper.Map<List<PlayerShortDto>>(playerService.GetOnlinePlayers());

            if (players != null)
                return Ok(players);

            return NotFound();
        }
    }
}
