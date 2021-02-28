using Microsoft.AspNetCore.Mvc;
using System.Net;
using static PrimesService.Primes;

namespace PrimesService.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PrimesController : ControllerBase
    {
        [HttpGet]
        [Route("/primes/{number}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult IsNumberPrime(int number)
        {
            var isPrime =  IsPrime(number);

            if (isPrime)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/primes")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<string> GetPrimesNumbers(string from, string to)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                return BadRequest();
            }

            if (int.TryParse(from, out var lowerBound) && int.TryParse(to, out var upperBound))
            {
                var primes = GetPrimeNumbers(lowerBound, upperBound);

                var response = "[" + string.Join(',', primes) + "]";

                return Ok(response);
            }

            return BadRequest();
        }
    }
}
