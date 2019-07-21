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
    public class ComplaintController : ApiController
    {
        private readonly IComplaintService complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            this.complaintService = complaintService;
        }

        public ComplaintController()
        {

        }

        /// <summary>
        /// Gets list of complaints
        /// </summary>
        /// <returns>JSON containing information about complaints</returns>
        [HttpGet]
        [Route("api/complaint")]
        public IHttpActionResult GetComplaints([FromUri] int player = -1, int number = 10)
        {
            var complaints = complaintService.GetComplaints(number, 0);
            var information = complaintService.GetGlobalInformationForComplaints(player);
            var result = new { complaints, information };

            return Ok(result);
        }

        /// <summary>
        /// Gets information about specific complaint
        /// </summary>
        /// <returns>JSON containing information about specific complaint</returns>
        [HttpGet]
        [Route("api/complaint/{complaintId}")]
        public IHttpActionResult GetComplaints(int complaintId)
        {
            var complaint = complaintService.GetComplaint(complaintId);

            if (complaint != null)
                return Ok(complaint);

            return NotFound();
        }
    }
}
