using Domain.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IHouseService
    {
        Task<IEnumerable<House>> GetHousesByPriceRange(int maxItems, string continuationToken, float priceFrom, float priceTo);
    }
}
