using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse_623434_project
{
    public static class ResponseHelper
    {
        public static async Task<HttpResponseData> BodyResponse<T>(T t, HttpStatusCode code, HttpRequestData req)
        {
            var json = JsonConvert.SerializeObject(t);
            var response = req.CreateResponse(code);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            await response.WriteStringAsync(json);
            return response;
        }
    }
}
