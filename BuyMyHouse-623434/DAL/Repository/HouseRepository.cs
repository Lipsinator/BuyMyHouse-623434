using DAL.Interface;
using Domain.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class HouseRepository : IHouseRepository
    {
        private HouseContext _HousesContext;

        public HouseRepository(HouseContext hContext)
        {
            _HousesContext = hContext;
        }

        public async Task<IEnumerable<House>> GetHousesByPriceRange(int maxItems, string continuationToken, float priceFrom, float priceTo)
        {
            var houses = _HousesContext.Houses.Where(h => h.Price > priceFrom && h.Price < priceTo);
            return houses;
        }
    }
}
