using System;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Utility
{
    public class Email
    {
        public static async void SendMail(EmailAddress to, string subject, string plainTextContent, string htmlContent,
            string b64Content = null, string attachmentName = null)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            var email = Environment.GetEnvironmentVariable("SENDGRID_EMAIL");

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(email, "BuyMyHouse-623434");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (!string.IsNullOrEmpty(b64Content) && !string.IsNullOrEmpty(attachmentName))
                msg.AddAttachment(attachmentName, b64Content, "application/pdf");

            await client.SendEmailAsync(msg);
        }
    }
}
