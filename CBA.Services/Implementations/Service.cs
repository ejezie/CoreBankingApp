using System;
using System.Security.Cryptography;
using CBA.Services.Interfaces;
using Microsoft.Extensions.Options;
using CBA.Core.Models;
//using System.Net.Mail;
using MailKit.Security;
//using System.Net.Mime;
using System.IO;
using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace CBA.Services.Implementations
{
    public class Service : IService
    {
        private readonly MailSettings _mailSettings;

        public Service(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public string GeneratePassword()
        {
            using RNGCryptoServiceProvider cryptRNGen = new();
            byte[] tokenBuffer = new byte[12];
            cryptRNGen.GetBytes(tokenBuffer);
            return Convert.ToBase64String(tokenBuffer);
        }

        public string GenerateUserName(string firstname, string secondname)
        {
            //Store the second name attach substring of first name.
            var possibleUsername = string.Format("{0}_{1}", secondname, firstname.Substring(0, 1));
            return possibleUsername;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, System.Net.Mime.ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

     
    }
}
