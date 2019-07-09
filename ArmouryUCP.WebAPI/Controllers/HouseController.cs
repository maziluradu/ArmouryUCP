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
    public class HouseController : ApiController
    {
        private readonly IHouseService houseService;

        public HouseController(IHouseService houseService)
        {
            this.houseService = houseService;
        }

        public HouseController()
        {

        }

        /// <summary>
        /// Gets house information for a specific owner
        /// </summary>
        /// <returns>JSON containing information about house for the specific owner</returns>
        [HttpGet]
        [Route("api/house/{owner}/multiple")]
        public IHttpActionResult GetHousesByOwner(string owner)
        {
            var houses = Mapper.Map<List<HouseDto>>(houseService.GetHouses(owner));
            return Ok(houses);
        }

        /// <summary>
        /// Gets house information for a specific owner
        /// </summary>
        /// <returns>JSON containing information about house for the specific owner</returns>
        [HttpGet]
        [Route("api/house/{id}")]
        public IHttpActionResult GetHouseById(int id)
        {
            var house = Mapper.Map<HouseCompleteDto>(houseService.GetHouse(id));
            return Ok(house);
        }

        /// <summary>
        /// Gets house with tenants
        /// </summary>
        /// <returns>JSON containing information about house</returns>
        [HttpGet]
        [Route("api/house/featuredHouse")]
        public IHttpActionResult GetHouseWithTenants()
        {
            var house = Mapper.Map<HouseCompleteDto>(houseService.GetHouseWithTenants());
            return Ok(house);
        }
        /// <summary>
        /// Gets partial information about houses on the server
        /// </summary>
        /// <returns>JSON containing information about multiple houses</returns>
        [HttpGet]
        [Route("api/house")]
        public IHttpActionResult GetHouses(int number = 10)
        {
            var initialHouses = houseService.GetHouses(number, 0);
            var houses = Mapper.Map<List<HouseDto>>(initialHouses);
            var information = houseService.GetGlobalInformationForHouses();
            var result = new { houses, information };

            return Ok(result);
        }

        /// <summary>
        /// Same as Route: api/house, but can start from specific page
        /// </summary>
        /// <returns>JSON containing information about multiple houses</returns>
        [HttpGet]
        [Route("api/house/paging/{page}")]
        public IHttpActionResult GetHousesStartingFromPage(int page, int number = 10)
        {
            var initialHouses = houseService.GetHouses(number, page * number);
            var houses = Mapper.Map<List<HouseDto>>(initialHouses);
            var information = houseService.GetGlobalInformationForHouses();
            var result = new { houses, information };

            return Ok(result);
        }

        /// <summary>
        /// Gets whole information about certain player. Only use this in critical cases - it outputs
        /// a lot of data.
        /// </summary>
        /// <param name="id">SQL ID of the user</param>
        /// <returns>JSON containing information about the player</returns>
        /*[HttpGet]
        [Route("api/player/{id}")]
        public IHttpActionResult GetPlayer(int id)
        {
            var player = Mapper.Map<PlayerDto>(playerService.GetPlayer(id));

            if (player != null)

                return Ok(player);

            return NotFound();
        }*/
    }
}
