using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using ShowroomService.Tests.Helpers;
using ShowroomService.Tests.Helpers.Cars;

namespace ShowroomService.Test.Tests
{
    [TestFixture]
    public class CarsApiTests
    {
        [TestCase("Saloon")]
        public async Task WhenSendingValidCarType_ApiShouldReturnOk(string type)
        {
            // Arrange, Act
            var response = await CarsGetApi.GetCatsAsync(type);

            // Assert
            Assert.That(response, Is.Not.Null, "The response status was not OK (200)");
        }

        [TestCase("Saloon")]
        [TestCase("Hatchback")]
        [TestCase("SUV")]
        public async Task WhenSendingValidCarType_ApiShouldReturnListOfValidObjects(string type)
        {
            // Arrange, Act
            var response = await CarsGetApi.GetCatsAsync(type);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null, "The response status was not OK (200)");
                var carTypes = response.Select(x => x.Type).Distinct();
                Assert.That(carTypes.Count(), Is.EqualTo(1), $"Cars api returned type(s) other then {type}" +
                                                             $"\n returned types are {string.Join("; ", carTypes)}");
                StringAssert.AreEqualIgnoringCase(carTypes.First(), type);
            });
        }

        [TestCase("Coupe")]
        [TestCase("Sedan")]
        [TestCase("Bike")]
        public async Task WhenSendingNotValidCarType_ApiShouldReturnListOfValidObjects(string type)
        {
            // Arrange, Act
            var response = await CarsGetApi.GetCatsAsync(type);

            Assert.That(response, Is.Null, "The response status was not NotFound (404)");
        }

        [TestCase(Method.Post)]
        [TestCase(Method.Put)]
        [TestCase(Method.Patch)]
        [TestCase(Method.Copy)]
        [TestCase(Method.Head)]
        [TestCase(Method.Options)]
        [TestCase(Method.Merge)]
        [TestCase(Method.Delete)]
        public async Task WhenSendingWrongMethod_ApiShouldReturnListOfValidObjects(Method method)
        {
            // Arrange, Act
            var type = "Saloon";
            RestClient client = new RestClient($"{Configurations.Configurations.Config.ApiUrl}/api/cars/{{type}}") { Options = { MaxTimeout = 5000 } };
            var request = RestHelper.InitRequest(method);
            request.AddUrlSegment("type", type);
            var response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Endpoint should block all requests except Get");
        }
    }
}
