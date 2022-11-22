namespace WeatherService.Api.Models.Responses
{
    public class ErrorResponse
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}