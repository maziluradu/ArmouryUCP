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
    public class VehicleController : ApiController
    {
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public VehicleController()
        {

        }

        /// <summary>
        /// Gets whole information about multiple players. Extremely inefficient. Only use this in
        /// critical cases - it outputs huge blocks of data.
        /// </summary>
        /// <returns>JSON containing information about multiple players</returns>
        [HttpGet]
        [Route("api/vehicle/{owner}")]
        public IHttpActionResult GetVehicleByOwner(string owner)
        {
            var vehicles = Mapper.Map<List<VehicleDto>>(vehicleService.GetVehicles(owner));
            return Ok(vehicles);
        }
    }
}
