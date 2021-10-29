using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Interface;

namespace BuyMyHouse_623434_project.Functions
{
    public class ServiceCustomersTimerTrigger
    {
        private readonly IBuyerInfoService _BuyerInfoService;
        private readonly ILogger<ServiceCustomersTimerTrigger> _Logger;

        public ServiceCustomersTimerTrigger(IBuyerInfoService buyerInfoService, ILogger<ServiceCustomersTimerTrigger> logger)
        {
            _BuyerInfoService = buyerInfoService;
            _Logger = logger;
        }

        [Function("ServiceCustomers")]
        public async Task ServiceCustomers([TimerTrigger("0 0 24 * * *")] MyInfo myTimer, FunctionContext context)
        {
            //This TimerTrigger generates a queue message at night for calculating mortgage.
            await _BuyerInfoService.CreateMortgageApplicationQueue();
            _Logger.LogInformation("Create Mortgageapplication queue messageds have been created");
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
