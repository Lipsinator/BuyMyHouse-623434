﻿using DAL.Interface;
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
    public class MortgageApplicationService : IMortgageApplicationService
    {
        private readonly IMortgageApplicationService _MortgageApplicationRepository;
        public MortgageApplicationService(IMortgageApplicationService mortgageApplicationRepository)
        {
            _MortgageApplicationRepository = mortgageApplicationRepository;
        }
        public async Task<MortgageApplication> CreateMortgageApplication(MortgageApplication mortgageApplication)
        {
            return await _MortgageApplicationRepository.CreateMortgageApplication(mortgageApplication);
        }

        public async Task<IEnumerable<MortgageApplication>> GetAllMortgageApplications()
        {
            return await _MortgageApplicationRepository.GetAllMortgageApplications();
        }
    }
}
