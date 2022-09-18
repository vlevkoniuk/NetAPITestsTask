using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using ShowroomService.Test.Configurations;
using ShowroomService.Test.Framework.Exceptions;

namespace ShowroomService.Tests.Helpers.Cars
{
    public static class CarsGetApi
    {
        private static readonly string _url = Configurations.Config.ApiUrl;

        private static RestClient _client = new RestClient($"{_url}/api/cars/{{type}}") {Options = { MaxTimeout = 5000}};

        private static readonly List<HttpStatusCode> _expectedStatuses = new List<HttpStatusCode>()
        {
            HttpStatusCode.OK,
            HttpStatusCode.NotFound
        };

        public static async Task<IEnumerable<ShowroomService.Model.Car>> GetCatsAsync(string type)
        {
            var request = RestHelper.InitRequest(Method.Get, "text/plain");
            request.AddUrlSegment("type", type);
            var response = await _client.GetAsync(request);
            if (_expectedStatuses.Contains(response.StatusCode))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    return JsonConvert.DeserializeObject<IEnumerable<ShowroomService.Model.Car>>(response.Content);
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;
            }
            else
            {
                throw new NotExpectedResponseException(response, _expectedStatuses);
            }

            return null;
        }
    }
}
