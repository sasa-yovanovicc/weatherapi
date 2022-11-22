using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherService.Api.Models;
using Xunit;

namespace WeatherService.Api.Controllers.UnitTests
{
    public class AstronomyControllerTests : BaseController
    {
        /// <summary>
        /// Test case for return ok.
        /// Date is optional and if is in wrong format weather api ignoring it.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="date">The date.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [InlineData("Stockholm", null, "Stockholm")]
        [InlineData("Stockholm", "2022-11-12", "Stockholm")]
        [InlineData("Stockholm, Sweden", null, "Stockholm")]
        [InlineData("Stockholm, Sweden", "2022-11-12", "Stockholm")]
        [InlineData("Stockholm, Stockholms Lan, Sweden", "2022-11-12", "Stockholm")]
        [InlineData("Sweden", null, "Stockholm")]
        [InlineData("Stockholm, Sweden", "202eeedd22-11-11", "Stockholm")]
        public async Task Get_ReturnsOk(string city, string date, string expectedResult)
        {
            // Arrange.
            var regex = "(1[012]|[1-9]):[0-5][0-9](\\s)?(?i)(AM|PM)";
            var controller = new AstronomyController(_config);

            // Act.
            var result = await controller.Get(city, date) as ObjectResult;
            var actualResult = result.Value;

            // Assert.
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);

            Assert.NotNull(actualResult);

            Assert.Equal(expectedResult, ((AstronomyModel)actualResult).City);
            Assert.True(((AstronomyModel)actualResult).Temperature.GetType() == typeof(double));

            Assert.NotNull(((AstronomyModel)actualResult).Sunrise);
            Assert.True(((AstronomyModel)actualResult).Sunrise.GetType() == typeof(string));
            Assert.Matches(regex, ((AstronomyModel)actualResult).Sunrise);
            Assert.NotNull(((AstronomyModel)actualResult).Sunset);
            Assert.True(((AstronomyModel)actualResult).Sunset.GetType() == typeof(string));
        }

        /// <summary>
        /// Test case no matching location.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="date">The date.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [InlineData("abcd", null, "No matching location found.")]
        [InlineData("abcd", "2022-11-12", "No matching location found.")]
        public async Task Get_BadRequest_NoMatchingLocation(string city, string date, string expectedResult)
        {
            // Arrange.
            var controller = new AstronomyController(_config);

            // Act.
            var result = await controller.Get(city, date) as ObjectResult;
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