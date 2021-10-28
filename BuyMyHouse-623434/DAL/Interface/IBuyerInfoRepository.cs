using Domain.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IBuyerInfoRepository
    {
        Task<BuyerInfo> CreateBuyerInfo (BuyerInfo buyerInfo);
        Task<IEnumerable<BuyerInfo>> GetAllBuyerInfo();
    }
}
