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
        [Route("api/vehicle/{owner}/multiple")]
        public IHttpActionResult GetVehicleByOwner(string owner)
        {
            var vehicles = Mapper.Map<List<VehicleDto>>(vehicleService.GetVehicles(owner));
            return Ok(vehicles);
        }
        /// <summary>
        /// Gets vehicles information for a specific owner
        /// </summary>
        /// <returns>JSON containing information about vehicle for the specific owner</returns>
        [HttpGet]
        [Route("api/vehicle/{id}")]
        public IHttpActionResult GetVehicleById(int id)
        {
            var vehicle = Mapper.Map<VehicleCompleteDto>(vehicleService.GetVehicle(id));
            return Ok(vehicle);
        }
        /// <summary>
        /// Gets partial information about vehicles on the server
        /// </summary>
        /// <returns>JSON containing information about multiple vehicles</returns>
        [HttpGet]
        [Route("api/vehicle")]
        public IHttpActionResult GetVehicles(int number = 10)
        {
            var initialVehicles = vehicleService.GetVehicles(number, 0);
            var vehicles = Mapper.Map<List<VehicleDto>>(initialVehicles);
            var information = vehicleService.GetGlobalInformationForVehicles();
            var result = new { vehicles, information };

            return Ok(result);
        }

        /// <summary>
        /// Same as Route: api/vehicle, but can start from specific page
        /// </summary>
        /// <returns>JSON containing information about multiple vehicles</returns>
        [HttpGet]
        [Route("api/vehicle/paging/{page}")]
        public IHttpActionResult GetVehiclesStartingFromPage(int page, int number = 10)
        {
            var initialVehicles = vehicleService.GetVehicles(number, number * page);
            var vehicles = Mapper.Map<List<VehicleDto>>(initialVehicles);
            var information = vehicleService.GetGlobalInformationForVehicles();
            var result = new { vehicles, information };

            return Ok(result);
        }
    }
}
