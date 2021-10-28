using Domain.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IMortgageApplicationRepository
    {
        Task<MortgageApplication> CreateMortgageApplication (MortgageApplication mortgageApplication);
        Task<IEnumerable<MortgageApplication>> GetAllMortgageApplications();
    }
}
