using Ecommerce.Application.Interfaces.Infrastructure;
using Ecommerce.Application.Models.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.EmailImplementation
{
    public class EmailService : IEmailService
    {
        // TODO: finish this service
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(EmailMessage email, string token)
        {            
            try
            {
                // crea el mensaje
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse(_emailSettings.Email));
                mail.To.Add(MailboxAddress.Parse(email.To));
                mail.Subject = email.Subject;
                mail.Body = new TextPart(TextFormat.Html) { Text = $"<body> <h5>Este mensaje está dirigido a {email.To}</h5> {email.Body} {_emailSettings.BaseUrlClient}/password/reset/{token} </body>" };

                // envía el email
                var client = new SmtpClient();
                client.Connect(_emailSettings.SmtpHost, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                client.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
                client.Send(mail);
                client.Disconnect(true);
                
                return await Task.FromResult(true);                
                

            }catch(Exception ex)
            {                
                _logger.LogError($"No se ha podido enviar el email, error: {ex.Message}");
                return await Task.FromResult(false);
            }
        }
    }
}
