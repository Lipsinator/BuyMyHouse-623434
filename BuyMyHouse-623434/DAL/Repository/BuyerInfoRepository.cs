using DAL.EFContext;
using DAL.Interface;
using Domain.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BuyerInfoRepository : IBuyerInfoRepository
    {
        private readonly BuyerInfoContext _BuyerInfoContext;

        public BuyerInfoRepository(BuyerInfoContext buyerInfoContext)
        {
            _BuyerInfoContext = buyerInfoContext;
        }

        public async Task AddBlobId(BuyerInfo buyerInfo)
        {
            _BuyerInfoContext.Update(buyerInfo);
        }

        public async Task<BuyerInfo> CreateBuyerInfo(BuyerInfo buyerInfo)
        {
            _BuyerInfoContext.BuyerInfos.Add(buyerInfo);
            _BuyerInfoContext.SaveChanges();
            return _BuyerInfoContext.BuyerInfos.Find(buyerInfo.id);
        }

        public async Task<IEnumerable<BuyerInfo>> GetAllBuyerInfo()
        {
            return _BuyerInfoContext.BuyerInfos.ToList();
        }

        public async Task<BuyerInfo> GetBuyerInfoById(string id)
        {
            return _BuyerInfoContext.BuyerInfos.Find(id);
        }


    }
}
