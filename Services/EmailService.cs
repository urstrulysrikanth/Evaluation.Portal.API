using Evaluation.Portal.API.Helpers;
using Evaluation.Portal.API.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluation.Portal.API.Services
{

    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);

        Task SendWelcomeEmailAsync(WelcomeRequest request);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;
        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            // email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

            InternetAddressList internetAddressList = new InternetAddressList();
            internetAddressList.AddRange(emailRequest.ToEmail.Select(email => new MailboxAddress(email, email)));

            email.To.AddRange(internetAddressList);
            email.Subject = emailRequest.Subject;
            var builder = new BodyBuilder();
            if (emailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in emailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = emailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText
                .Replace("{username}", request.UserName)
                .Replace("{email}", request.Email)
                .Replace("{portal}", "http://localhost:8080")
                .Replace("{password}", CryptoEngine.Decrypt(request.Password, _mailSettings.HashKey));
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.Subject = $"Welcome {request.UserName}";
            var builder = new BodyBuilder
            {
                HtmlBody = MailText
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
