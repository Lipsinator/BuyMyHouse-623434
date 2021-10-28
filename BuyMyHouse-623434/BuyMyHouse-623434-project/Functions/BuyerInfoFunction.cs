using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Domain.DBModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Interface;

namespace BuyMyHouse_623434_project
{
    public class BuyerInfoFunction
    {
        private readonly IBuyerInfoService _BuyerInfoService;
        private readonly ILogger<BuyerInfo> _Logger;
        public BuyerInfoFunction(ILogger<BuyerInfo> logger, IBuyerInfoService buyerInfoService)
        {
            _Logger = logger;
            _BuyerInfoService = buyerInfoService;
        }

        [Function("CreateBuyerInfo")]
        public async Task<HttpResponseData> CreateBuyerInfo([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();
                var buyerInfo = JsonConvert.DeserializeObject<BuyerInfo>(content);

                return await ResponseHelper.BodyResponse(await _BuyerInfoService.CreateBuyerInfo(buyerInfo), 
                    HttpStatusCode.Created, req);
            }
            catch (Exception e)
            {
                _Logger.LogError("{Error}", e.Message);
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.Headers.Add("Content-Type", "text/plain");
                await response.WriteStringAsync("Oops something went wrong.");
                return response;
            }
        }
    }
}
