using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherService.Api.Models;
using Xunit;

namespace WeatherService.Api.Controllers.UnitTests
{
    public class WeatherControllerTests : BaseController
    {
        /// <summary>
        /// Test case for return ok.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [InlineData("Stockholm", "Stockholm")]
        [InlineData("Stockholm, Sweden", "Stockholm")]
        [InlineData("Stockholm, Stockholms Lan, Sweden", "Stockholm")]
        [InlineData("Sweden", "Stockholm")]
        public async Task Get_ReturnsOk(string city, string expectedResult)
        {
            // Arrange.
            var controller = new WeatherController(_config);

            // Act.
            var result = await controller.Get(city) as ObjectResult;
            var actualResult = result.Value;

            // Assert.
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);

            Assert.NotNull(actualResult);

            Assert.Equal(expectedResult, ((WeatherModel)actualResult).City);
            Assert.True(((WeatherModel)actualResult).Temperature.GetType() == typeof(double));
        }

        /// <summary>
        /// Test case no matching location.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [InlineData("abcd", "No matching location found.")]
        public async Task Get_BadRequest_NoMatchingLocation(string city, string expectedResult)
        {
            // Arrange.
            var controller = new WeatherController(_config);

            // Act.
            var result = await controller.Get(city) as ObjectResult;
            var actualResult = result.Value;

            // Assert.
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

            Assert.NotNull(actualResult);

            Assert.Equal(expectedResult, ((ErrorMessage)actualResult).Message);
        }
    }
}