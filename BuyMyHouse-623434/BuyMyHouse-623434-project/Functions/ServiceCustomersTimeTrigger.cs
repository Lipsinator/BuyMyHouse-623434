using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceLayer.Interface;

namespace BuyMyHouse_623434_project.Functions
{
    public class ServiceCustomersTimeTrigger
    {
        private readonly IBuyerInfoService _BuyerInfoService;

        public ServiceCustomersTimeTrigger(IBuyerInfoService buyerInfoService)
        {
            _BuyerInfoService = buyerInfoService;
        }

        [Function("ServiceCustomers")]
        public async Task ServiceCustomers([TimerTrigger("* * * * *")] MyInfo myTimer, FunctionContext context)
        {
            var buyerInfos = await _BuyerInfoService.GetAllBuyerInfo();

            string ServiceBusConnectString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            string QueueName = Environment.GetEnvironmentVariable("QueueName");
            if (!string.IsNullOrEmpty(QueueName))
            {
                IQueueClient client = new QueueClient(ServiceBusConnectString, QueueName);

                // Send buyerInfo ids to the service bus so the listener can process the requests one at a time.
                foreach (var buyerInfo in buyerInfos)
                {
                    var messageBody = JsonConvert.SerializeObject(buyerInfo.id);
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await client.SendAsync(message);
                }
            }  
        }
    }
    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
