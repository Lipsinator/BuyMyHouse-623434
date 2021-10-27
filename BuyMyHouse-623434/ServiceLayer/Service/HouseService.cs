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
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _HouseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _HouseRepository = houseRepository;
        }

        public async Task<IEnumerable<House>> GetHousesByPriceRange(int maxItems, string continuationToken, float priceFrom, float priceTo)
        {
            return await _HouseRepository.GetHousesByPriceRange(maxItems, continuationToken, priceFrom, priceTo);
        }
    }
}
