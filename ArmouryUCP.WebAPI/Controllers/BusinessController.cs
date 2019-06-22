﻿using ArmouryUCP.WebAPI.Models.Dtos;
using ArmouryUCP.WebAPI.Services.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace ArmouryUCP.WebAPI.Controllers
{
    [EnableCors(origins: "http://panel.armoury.ro", headers: "*", methods: "*")]
    public class BusinessController : ApiController
    {
        private readonly IBusinessService businessService;

        public BusinessController(IBusinessService businessService)
        {
            this.businessService = businessService;
        }

        public BusinessController()
        {

        }

        /// <summary>
        /// Gets whole information about multiple players. Extremely inefficient. Only use this in
        /// critical cases - it outputs huge blocks of data.
        /// </summary>
        /// <returns>JSON containing information about multiple players</returns>
        [HttpGet]
        [Route("api/business/{owner}")]
        public IHttpActionResult GetBusinessByOwner(string owner)
        {
            var businesses = Mapper.Map<List<BusinessDto>>(businessService.GetBusinesses(owner));
            return Ok(businesses);
        }

        /// <summary>
        /// Gets partial information about businesses on the server
        /// </summary>
        /// <returns>JSON containing information about multiple businesses</returns>
        [HttpGet]
        [Route("api/business")]
        public IHttpActionResult GetBusinesses(int number = 10)
        {
            var initialBusinesses = businessService.GetBusinesses(number, 0);
            var businesses = Mapper.Map<List<BusinessDto>>(initialBusinesses);
            var information = businessService.GetGlobalInformationForBusinesses();
            var result = new { businesses, information };

            return Ok(result);
        }

        /// <summary>
        /// Same as Route: api/business, but can start from specific page
        /// </summary>
        /// <returns>JSON containing information about multiple businesses</returns>
        [HttpGet]
        [Route("api/business/paging/{page}")]
        public IHttpActionResult GetBusinessesStartingFromPage(int page, int number = 10)
        {
            var initialBusinesses = businessService.GetBusinesses(number, page * number);
            var businesses = Mapper.Map<List<BusinessDto>>(initialBusinesses);
            var information = businessService.GetGlobalInformationForBusinesses();
            var result = new { businesses, information };

            return Ok(result);
        }
    }
}
