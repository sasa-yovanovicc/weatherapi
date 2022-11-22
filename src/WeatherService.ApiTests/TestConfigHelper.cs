using Microsoft.Extensions.Configuration;

namespace WeatherService.Api.UnitTests.Helpers
{
    /// <summary>
    /// Helper class, gets configuration.
    /// </summary>
    internal class TestConfigHelper
    {
        /// <summary>
        /// ConfigurationBuilder.
        /// Load environment parameters in the order production, development, then environment.
        /// Every next loaded parameters override previous.
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}