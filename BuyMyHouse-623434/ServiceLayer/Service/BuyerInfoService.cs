using DAL.Helpers;
using DAL.Interface;
using DAL.Repository;
using Domain.DBModels;
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
        public BuyerInfoService(IBuyerInfoRepository buyerInfoRepository, IBlobService blobService)
        {
            _BlobService = blobService;
            _BuyerInfoRepository = buyerInfoRepository;
        }
        public async Task<BuyerInfo> CreateBuyerInfo(BuyerInfo buyerInfo)
        {
            return await _BuyerInfoRepository.CreateBuyerInfo(buyerInfo);
        }

        public async Task CreateMortgageApplication(string myQueueItem)
        {
            
            BuyerInfo buyerInfo = await _BuyerInfoRepository.GetBuyerInfoById(myQueueItem);
            float amountToBorrow = buyerInfo.YearlyIncome * 10;
            var pdf =  PDF.CreatePDF(buyerInfo, amountToBorrow);
            var fileName =  await _BlobService.CreateFile(Convert.ToBase64String(pdf), Guid.NewGuid()+".pdf");
            buyerInfo.BlobId = fileName;
            await _BuyerInfoRepository.AddBlobId(buyerInfo);
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
