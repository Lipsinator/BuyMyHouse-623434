using DAL.Interface;
using DAL.Repository;
using Domain.DBModels;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class BuyerInfoService : IBuyerInfoService
    {
        private readonly IBuyerInfoRepository _BuyerInfoRepository;
        public BuyerInfoService(IBuyerInfoRepository buyerInfoRepository)
        {
            _BuyerInfoRepository = buyerInfoRepository;
        }
        public async Task<BuyerInfo> CreateBuyerInfo(BuyerInfo buyerInfo)
        {
            return await _BuyerInfoRepository.CreateBuyerInfo(buyerInfo);
        }

        public async Task<IEnumerable<BuyerInfo>> GetAllBuyerInfo()
        {
            return await _BuyerInfoRepository.GetAllBuyerInfo();
        }
    }
}
