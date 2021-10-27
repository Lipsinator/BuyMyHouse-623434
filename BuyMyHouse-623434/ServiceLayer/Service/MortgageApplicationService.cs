using Domain.DBModels;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class MortgageApplicationService : IMortgageApplcationService
    {
        public Task<MortgageApplication> CreateUserProfile(MortgageApplication mortgageApplication)
        {
            throw new NotImplementedException();
        }
    }
}
