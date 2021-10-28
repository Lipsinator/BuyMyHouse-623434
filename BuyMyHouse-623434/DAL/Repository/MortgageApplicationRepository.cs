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
    public class MortgageApplicationRepository : IMortgageApplicationRepository
    {
        private readonly MortgageApplicationContext _MortegageApplcationContext;

        public MortgageApplicationRepository(MortgageApplicationContext mortgageApplicationContext)
        {
            _MortegageApplcationContext = mortgageApplicationContext;
        }
        
        public async Task<MortgageApplication> CreateMortgageApplication(MortgageApplication mortgageApplication)
        {
            _MortegageApplcationContext.MortgageApplications.Add(mortgageApplication);
            _MortegageApplcationContext.SaveChanges();
            return _MortegageApplcationContext.MortgageApplications.Single(m => m.id == mortgageApplication.id);
        }

        public async Task<IEnumerable<MortgageApplication>> GetAllMortgageApplications()
        {
            return _MortegageApplcationContext.MortgageApplications.ToList();
        }
    }
}
