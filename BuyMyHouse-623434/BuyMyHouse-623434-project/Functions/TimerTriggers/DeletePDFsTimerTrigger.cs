using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Interface;

namespace BuyMyHouse_623434_project.Functions.TimerTriggers
{
    public class DeletePDFsTimerTrigger
    {
        private readonly IBuyerInfoService _BuyerInfoService;
        private readonly ILogger<DeletePDFsTimerTrigger> _Logger;

        public DeletePDFsTimerTrigger(IBuyerInfoService buyerInfoService, ILogger<DeletePDFsTimerTrigger> logger)
        {
            _BuyerInfoService = buyerInfoService;
            _Logger = logger;
        }

        [Function("DeletePDFsTimerTrigger")]
        public async Task<HttpResponse> DeletePDFs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {

            //[Function("DeletePDFsTimerTrigger")]
            //public async Task DeletePDFs([TimerTrigger("0 * * * * *")] MyInfo myTimer, FunctionContext context)
            //{
            await _BuyerInfoService.DeleteBlobIdFromBuyerInfo();
            _Logger.LogInformation("The PDF blobIds for generated mortgages have now been deleted");
            return null;
        }
    }
}
