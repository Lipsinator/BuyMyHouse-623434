using DAL.Helpers;
using DAL.Interface;
using Domain.DBModels;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace ServiceLayer.Service
{
    public class MailService : IMailService
    {
        private readonly IBlobService _BlobService;
        private readonly IBuyerInfoRepository _BuyerInfoRepository;
        private readonly ILogger<MailService> _Logger;

        public MailService(IBuyerInfoRepository buyerInfoRepository, IBlobService blobService, ILogger<MailService> logger)
        {
            _BuyerInfoRepository = buyerInfoRepository;
            _BlobService = blobService;
            _Logger = logger;
        }

        public async Task SendMails(IEnumerable<BuyerInfo> buyerInfoList)
        {
            foreach (var buyerInfo in buyerInfoList)
            {
                _Logger.LogInformation($"Sending emails to all users in batched list.");
                var pdfBA = await _BlobService.GetBlobFromServer(buyerInfo.BlobId);
                var pdfB64 = Convert.ToBase64String(pdfBA);

                Email.SendMail(new EmailAddress(buyerInfo.Email), "Your mortgage application", "Your mortgage application",
                    "Your mortgage application", pdfB64, "Mortgage");
            }
        }
    }
}
