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
        
        public async Task<BuyerInfo> CreateBuyerInfo(BuyerInfo buyerInfo)
        {
            _BuyerInfoContext.BuyerInfos.Add(buyerInfo);
            _BuyerInfoContext.SaveChanges();
            return _BuyerInfoContext.BuyerInfos.Single(b => b.id == buyerInfo.id);
        }

        public async Task<IEnumerable<BuyerInfo>> GetAllBuyerInfo()
        {
            return _BuyerInfoContext.BuyerInfos.ToList();
        }
    }
}
