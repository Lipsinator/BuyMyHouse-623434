using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ServiceLayer.Interface;
using Utility;

namespace BuyMyHouse_623434_project.Functions
{
    public class SendMailTimerTrigger
    {
        private readonly IBuyerInfoService _BuyerInfoService;
        private readonly IMailService _MailService;
        private readonly ILogger<SendMailTimerTrigger> _Logger;

        public SendMailTimerTrigger(ILogger<SendMailTimerTrigger> logger, IBuyerInfoService buyerInfoService, IMailService mailService)
        {
            _Logger = logger;
            _BuyerInfoService = buyerInfoService;
            _MailService = mailService;
        }

        //[Function("SendMailTimerTrigger")]
        //public async Task SendMail([TimerTrigger("0 * * * * *")] MyInfo myTimer, FunctionContext context)
        //{
        [Function("SendMailTimerTrigger")]
        public async Task<HttpResponse> SendMail(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var buyerInfos = await _BuyerInfoService.GetAllBuyerInfo();
            
            // Devide the objects into smaller batches so we can use more threads at the same time.
            var batchedBuyerInfo = Batching.CreateBatch(buyerInfos, int.Parse(Environment.GetEnvironmentVariable("BATCH_SIZE")));

            // chunked by N, so N threads are started that each send emails
            foreach (var buyerInfoList in batchedBuyerInfo)
            {
                var thread = StartSendMailThread(buyerInfoList);
            }
            return null;
        }

        private Thread StartSendMailThread(IEnumerable<BuyerInfo> buyerInfoList)
        {
            var t = new Thread(() => _MailService.SendMails(buyerInfoList));
            t.Start();
            return t;
        }
    }
}
