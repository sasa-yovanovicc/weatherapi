using Microsoft.AspNetCore.Mvc;
using WeatherService.Api.Models;
using WeatherService.Api.Models.Responses;

namespace WeatherService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IConfiguration _config;

        public WeatherController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Gets weather for the specified city.
        /// </summary>
        /// <param name="city">The city. Can be "City, Region, Location" in any combination or any of these geographic locations.</param>
        /// <returns></returns>
        [HttpGet(Name = "GetWeatherCurrent")]
        public async Task<IActionResult> Get([FromQuery] string city)
        {
            var url = _config.GetValue<string>("WeatherApi:BaseUrl");
            var key = _config.GetValue<string>("WeatherApi:ApiKey");

            var requestURIWeather = $"{url}current.json?key={key}&q={city}&aqi=no";

            HttpClient httpClient = new();

            var response = await httpClient.GetAsync(requestURIWeather);

            if (response.IsSuccessStatusCode)
            {
                var weatherResponse = response.Content.ReadFromJsonAsync<WeatherResponse>().GetAwaiter().GetResult();

                var weather = new WeatherModel
                {
                    City = weatherResponse.location.name,
                    Region = weatherResponse.location.region,
                    Country = weatherResponse.location.country,
                    LocalTime = Convert.ToDateTime(weatherResponse.location.localtime),
                    Temperature = weatherResponse.current.temp_c
                };

                return StatusCode(StatusCodes.Status200OK, weather);
            }
            else
            {
                var r = response.Content.ReadFromJsonAsync<ErrorResponse>().GetAwaiter().GetResult();
                return BadRequest(new ErrorMessage { Message = r.error.message });
            }
        }
    }
}