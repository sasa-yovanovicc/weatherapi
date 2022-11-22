using Microsoft.AspNetCore.Mvc;
using WeatherService.Api.Models;
using Xunit;

namespace WeatherService.Api.Controllers.UnitTests
{
    public class StatusControllerTests : BaseController
    {
        /// <summary>
        /// Gets the status returns ok.
        /// </summary>
        [Fact]
        public async Task Get_Status_ReturnsOk()
        {
            var controller = new StatusController();

            var result = controller.GetStatus();

            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);

            var model = objectResult.Value as InfoMessage;
            Assert.NotNull(model);

            Assert.Equal("ok", model.Message);
        }
    }
}