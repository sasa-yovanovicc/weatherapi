using Microsoft.Extensions.Configuration;

using WeatherService.Api.UnitTests.Helpers;

namespace WeatherService.Api.Controllers.UnitTests
{
    /// <summary>
    /// Base  Controller test class, gets IConfiguration
    /// </summary>
    public class BaseController
    {
        public IConfiguration _config = TestConfigHelper.GetIConfigurationRoot();
    }
}