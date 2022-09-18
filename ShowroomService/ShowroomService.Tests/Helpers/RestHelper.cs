using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using RestSharp;

namespace ShowroomService.Tests.Helpers
{
    public static class RestHelper
    {
        public static RestRequest InitRequest(Method method, string acceptHeader = "text/json")
        {
            RestRequest request = new RestRequest();
            request.Method = method;
            request.AddHeader("Accept", acceptHeader);
            return request;
        }
    }
}
