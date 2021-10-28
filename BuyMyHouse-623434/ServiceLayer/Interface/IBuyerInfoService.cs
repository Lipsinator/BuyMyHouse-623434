using Domain.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IBuyerInfoService
    {
        Task<BuyerInfo> CreateBuyerInfo(BuyerInfo buyerInfo);
        Task<IEnumerable<BuyerInfo>> GetAllBuyerInfo();
    }
}
