namespace WeatherService.Api.Models.Responses
{
    /// <summary>
    /// Weather Api
    /// </summary>
    public class AstronomyResponse
    {
        public Location location { get; set; }
        public Astronomy astronomy { get; set; }
    }

    public class Astro
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string moonrise { get; set; }
        public string moonset { get; set; }
        public string moon_phase { get; set; }
        public string moon_illumination { get; set; }
    }

    public class Astronomy
    {
        public Astro astro { get; set; }
    }
}