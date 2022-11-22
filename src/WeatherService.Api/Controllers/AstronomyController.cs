using Microsoft.AspNetCore.Mvc;
using WeatherService.Api.Helpers;
using WeatherService.Api.Models;
using WeatherService.Api.Models.Responses;

namespace WeatherService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AstronomyController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AstronomyController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Gets weather and sunrise/sunset data for the specified city.
        /// </summary>
        /// <param name="city">The city. Can be "City, Region, Location" in any combination or any of these geographic locations.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        [HttpGet(Name = "GetAstronomyCurrent")]
        public async Task<IActionResult> Get([FromQuery] string city, string? date)
        {
            date = Helper.GetDateString(date);

            var url = _config.GetValue<string>("WeatherApi:BaseUrl");
            var key = _config.GetValue<string>("WeatherApi:ApiKey");

            var requestURIWeather = $"{url}current.json?key={key}&q={city}&aqi=no";
            var requestURIAstro = $"{url}astronomy.json?key={key}&q={city}&dt={date}";

            HttpClient httpClient = new();

            var responseAstro = await httpClient.GetAsync(requestURIAstro);

            var responseWeather = await httpClient.GetAsync(requestURIWeather);

            if (responseWeather.IsSuccessStatusCode && responseAstro.IsSuccessStatusCode)
            {
                var astroResponse = responseAstro.Content.ReadFromJsonAsync<AstronomyResponse>().GetAwaiter().GetResult();
                var weatherResponse = responseWeather.Content.ReadFromJsonAsync<WeatherResponse>().GetAwaiter().GetResult();

                var weatherAstronomy = new AstronomyModel
                {
                    City = weatherResponse.location.name,
                    Region = weatherResponse.location.region,
                    Country = weatherResponse.location.country,
                    LocalTime = Convert.ToDateTime(weatherResponse.location.localtime),
                    Temperature = weatherResponse.current.temp_c,
                    Sunrise = astroResponse.astronomy.astro.sunrise,
                    Sunset = astroResponse.astronomy.astro.sunset
                };

                return StatusCode(StatusCodes.Status200OK, weatherAstronomy);
            }
            else
            {
                ErrorResponse res;
                if (!responseWeather.IsSuccessStatusCode)
                {
                    res = responseWeather.Content.ReadFromJsonAsync<ErrorResponse>().GetAwaiter().GetResult();
                }
                else
                {
                    res = responseAstro.Content.ReadFromJsonAsync<ErrorResponse>().GetAwaiter().GetResult();
                }

                return BadRequest(new ErrorMessage { Message = res.error.message });
            }
        }
    }
}