using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using RestSharp;

namespace ShowroomService.Test.Framework.Exceptions
{
    public class NotExpectedResponseException : Exception
    {
        public NotExpectedResponseException(RestResponse response, HttpStatusCode expectedCode) : base($"Received not expected response from the API" +
            $"\nAPI response status is {response.StatusCode}, with the following message {response.Content}" +
            $"\nExpected code is {expectedCode}")
        {
        }

        public NotExpectedResponseException(RestResponse response, IEnumerable<HttpStatusCode> expectedCodes) : base($"Received not expected response from the API" +
            $"\nAPI response status is {response.StatusCode}, with the following message {response.Content}" +
            $"\nExpected codes are {string.Join("; ", expectedCodes.Select(x => x.ToString()))}")
        {
        }
    }
}
