using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Interface;

namespace BuyMyHouse_623434_project
{
    public class HouseFunctions
    {
        private readonly IHouseService _HouseService;
        public HouseFunctions(IHouseService houseService)
        {
            _HouseService = houseService;
        }

        [Function("GetHousesByPriceRange")]
        public async Task<HttpResponseData> GetHousesByPriceRangeAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext, int maxItems, string continuationToken, float priceFrom, float priceTo)
        {
            var houses = await _HouseService.GetHousesByPriceRange(maxItems, continuationToken, priceFrom, priceTo);
           
            var json = JsonConvert.SerializeObject(houses);
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            await response.WriteStringAsync(json);

            return response;
        }
    }
}
