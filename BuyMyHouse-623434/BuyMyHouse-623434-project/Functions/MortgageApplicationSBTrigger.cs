using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BuyMyHouse_623434_project.Functions
{
    public class MortgageApplicationSBTrigger
    {
        [Function("MortgageApplicationSBTrigger")]
        public void ProcessMortgageApplications([ServiceBusTrigger("mortgageapplications", Connection = "ServiceBusConnectionString")] string myQueueItem, FunctionContext context)
        {

            var logger = context.GetLogger("MortgageApplicationSBTrigger");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
