using Microsoft.AspNetCore.Mvc;
using WeatherService.Api.Models;

namespace WeatherService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetStatus()
        {
            return StatusCode(StatusCodes.Status200OK, new InfoMessage { Message = "ok" });
        }
    }
}