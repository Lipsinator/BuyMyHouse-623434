using Domain.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IMortgageApplicationService
    {
        Task<MortgageApplication> CreateMortgageApplication(MortgageApplication mortgageApplication);
        Task<IEnumerable<MortgageApplication>> GetAllMortgageApplications();
    }
}
