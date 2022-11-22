namespace WeatherService.Api.Helpers
{
    public class Helper
    {
        /// <summary>
        /// Gets the date string.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string GetDateString(string? date)
        {
            return date ?? DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}