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
    public class MortgageApplicationFunction
    {
        private readonly IMortgageApplcationService _MortgageApplicationService;
        private readonly ILogger<MortgageApplication> _Logger;
        public MortgageApplicationFunction(ILogger<MortgageApplication> logger, IMortgageApplcationService mortgageApplcationService)
        {
            _Logger = logger;
            _MortgageApplicationService = mortgageApplcationService;
        }

        [Function("CreateMortgageApplcation")]
        public async Task<HttpResponseData> CreateMortgageApplcation([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();
                var mortgageApplication = JsonConvert.DeserializeObject<MortgageApplication>(content);

                return await ResponseHelper.BodyResponse(await _MortgageApplicationService.CreateMortgageApplication(mortgageApplication), 
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
