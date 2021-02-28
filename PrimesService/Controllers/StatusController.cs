using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PrimesService.Controllers
{
    [ApiController]
    [Route("api/v1/status")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult GetStatus()
        {
            return Ok("Primes Web Service by Bilotska Karyna");
        }
    }
}
