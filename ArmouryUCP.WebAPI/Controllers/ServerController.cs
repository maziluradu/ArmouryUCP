using ArmouryUCP.WebAPI.Services.Interfaces;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ArmouryUCP.WebAPI.Controllers
{
    [EnableCors(origins: "http://panel.armoury.ro", headers: "*", methods: "*")]
    public class ServerController : ApiController
    {
        private readonly IServerService serverService;

        public ServerController(IServerService serverService)
        {
            this.serverService = serverService;
        }

        public ServerController()
        {

        }

        /// <summary>
        /// Gets main information about the server's state
        /// </summary>
        /// <returns>JSON containing information about the server's state</returns>
        [HttpGet]
        [Route("api/server")]
        public IHttpActionResult GetServerInformation()
        {
            return Ok(serverService.GetServerInformation());
        }

        /// <summary>
        /// Gets main news about the server
        /// </summary>
        /// <returns>JSON containing information about the server's news</returns>
        [HttpGet]
        [Route("api/server/news")]
        public IHttpActionResult GetServerNews()
        {
            return Ok(serverService.GetServerNews());
        }
    }
}
