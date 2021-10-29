using System;
using System.Threading.Tasks;
using Domain.DBModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Interface;
using Utility;

namespace BuyMyHouse_623434_project.Functions
{
    public class ServiceBusTrigger
    {
        private readonly IBuyerInfoService _BuyerInfoService;
        public ServiceBusTrigger(IBuyerInfoService buyerInfoService)
        {
            _BuyerInfoService = buyerInfoService;
        }

        [Function("ServiceBusTrigger")]
        public async Task ProcessMortgageApplications([ServiceBusTrigger("mortgageapplications", Connection = "ServiceBusConnectionString")] string myQueueItem, FunctionContext context)
        {
            try
            {
                var buyerInfoId = JsonConvert.DeserializeObject<string>(myQueueItem);
                await _BuyerInfoService.CreateMortgageApplication(buyerInfoId);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
