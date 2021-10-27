using DAL.Interface;
using Domain.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class MortgageApplicationRepository : IMortgageApplicationRepository
    {
        public Task<MortgageApplication> CreateMortgageApplication(MortgageApplication mortgageApplication)
        {
            throw new NotImplementedException();
        }
    }
}
