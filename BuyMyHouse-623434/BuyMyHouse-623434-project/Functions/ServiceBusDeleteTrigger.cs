using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Interface;

namespace BuyMyHouse_623434_project.Functions
{
    public class ServiceBusDeleteTrigger
    {
        private readonly IBuyerInfoService _BuyerInfoService;
        public ServiceBusDeleteTrigger(IBuyerInfoService buyerInfoService)
        {
            _BuyerInfoService = buyerInfoService;
        }

        [Function("ServiceBusDeleteTrigger")]
        public async Task DeleteTrigger([ServiceBusTrigger("deletequeue", Connection = "ServiceBusConnectionString")] string myQueueItem, FunctionContext context)
        {
            // This trigger is triggered when the access time of a blob object has passed.
            var buyerInfoBlobId = JsonConvert.DeserializeObject<string>(myQueueItem);
            await _BuyerInfoService.DeleteMortage(buyerInfoBlobId);
        }
    }
}
