using DAL.Helpers;
using DAL.Interface;
using DAL.Repository;
using Domain.DBModels;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace ServiceLayer.Service
{
    public class BuyerInfoService : IBuyerInfoService
    {
        private readonly IBuyerInfoRepository _BuyerInfoRepository;
        private readonly IBlobService _BlobService;
        private readonly ILogger<BuyerInfoService> _Logger;
        public BuyerInfoService(IBuyerInfoRepository buyerInfoRepository, IBlobService blobService, ILogger<BuyerInfoService> logger)
        {
            _BlobService = blobService;
            _BuyerInfoRepository = buyerInfoRepository;
            _Logger = logger;
        }
        public async Task<BuyerInfo> CreateBuyerInfo(BuyerInfo buyerInfo)
        {
            return await _BuyerInfoRepository.CreateBuyerInfo(buyerInfo);
        }

        public async Task CreateMortgageApplicationQueue()
        {
            var buyerInfos = await GetAllBuyerInfo();

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

        public async Task CreateMortgageApplication(string myQueueItem)
        {         
            BuyerInfo buyerInfo = await _BuyerInfoRepository.GetBuyerInfoById(myQueueItem);
            float amountToBorrow = buyerInfo.YearlyIncome * 10;
            var pdf =  PDF.CreatePDF(buyerInfo, amountToBorrow);
            var fileName =  await _BlobService.CreateFile(Convert.ToBase64String(pdf), Guid.NewGuid()+".pdf");
            buyerInfo.BlobId = fileName;
            await _BuyerInfoRepository.UpdateBuyerInfo(buyerInfo);
        }

        public async Task DeleteBlobIdFromBuyerInfo()
        {
            var buyerInfos = await GetAllBuyerInfo();

            string ServiceBusConnectString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            string QueueName = Environment.GetEnvironmentVariable("DeleteQueueName");
            if (!string.IsNullOrEmpty(QueueName))
            {
                IQueueClient client = new QueueClient(ServiceBusConnectString, QueueName);

                // Send buyerInfo ids to the service bus so the listener can process the requests one at a time.
                foreach (var buyerInfo in buyerInfos)
                {
                    var messageBody = JsonConvert.SerializeObject(buyerInfo.BlobId);
                    buyerInfo.BlobId = "";

                    await _BuyerInfoRepository.UpdateBuyerInfo(buyerInfo);
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await client.SendAsync(message);
                }
            }
        }

        public async Task DeleteMortage(string buyerInfoBlobId)
        {
            await _BlobService.DeleteBlobFromServer(buyerInfoBlobId);
            _Logger.LogInformation("PDF blob has been deleted from blobl storage.");
        }

        public async Task<IEnumerable<BuyerInfo>> GetAllBuyerInfo()
        {
            return await _BuyerInfoRepository.GetAllBuyerInfo();
        }

        public async Task<BuyerInfo> GetBuyerInfoById(string id)
        {
            return await _BuyerInfoRepository.GetBuyerInfoById(id);
        }
    }
}
