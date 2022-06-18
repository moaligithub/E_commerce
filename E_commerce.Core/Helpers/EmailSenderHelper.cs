using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using E_commerce.Core.Settings;

namespace E_commerce.Core
{
    /// <summary>
    ///     Use This class for send mail message
    /// </summary>
    public class EmailSenderHelper : IEmailSender
    {
        private readonly MailSetting _mailsetting;

        public EmailSenderHelper(IOptions<MailSetting> mailsetting)
        {
            _mailsetting = mailsetting.Value;
        }

        /// <summary>
        ///     Use This method for send mail message by Mimekit Package
        /// </summary>
        /// <param name="email">This Parameter represent the E-mail Address is to sender</param>
        /// <param name="subject">This Paramter represent the subject that being send in mail</param>
        /// <param name="htmlMessage">This parameter represent the body message</param>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Configure The Message
            MimeMessage message = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailsetting.Email),
                Subject = subject
            };

            message.To.Add(MailboxAddress.Parse(email));

            BodyBuilder builder = new BodyBuilder();

            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();
            message.From.Add(new MailboxAddress(_mailsetting.DisplayName, _mailsetting.Email));

            // Send The Message
            using var stm = new SmtpClient();
            stm.Connect(_mailsetting.Host, _mailsetting.Port, SecureSocketOptions.StartTls);
            stm.Authenticate(_mailsetting.Email, _mailsetting.Password);
            await stm.SendAsync(message);
            stm.Disconnect(true);
        }
    }
}
