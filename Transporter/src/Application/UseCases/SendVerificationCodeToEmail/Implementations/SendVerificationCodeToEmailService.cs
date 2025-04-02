using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using src.Application.UseCases.SendVerificationCodeToEmail.Interfaces;
using MailKit.Net.Smtp;

namespace src.Application.UseCases.SendVerificationCodeToEmail.Implementations
{
    public class SendVerificationCodeToEmailService : ISendVerificationCodeToEmailService
    {
        private readonly MimeMessage _message;
        private readonly IConfiguration _configuration;

        public SendVerificationCodeToEmailService(MimeMessage message, IConfiguration configuration)
        {
            _message = message;
            _configuration = configuration;
        }

        public async Task SentAsync(string email, string code, DateTime CreatedAt, DateTime ExpirationDate)
        {
            var fromEmail = _configuration["OM_WAY_EMAIL"];
            var appPassword = _configuration["OM_WAY_EMAIL_PASSWORD"];
            if (string.IsNullOrEmpty(fromEmail))
            {
                throw new InvalidOperationException("The sender's email address is not configured.");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException("The recipient's email cannot be null or empty.");
            }

            _message.From.Add(new MailboxAddress("OmWay", fromEmail));
            _message.To.Add(new MailboxAddress("Cliente", email));
            _message.Subject = "Verification Code";
            _message.Body = new TextPart("plain")
            {
                Text = @"Hey, here is your verification code: " + code + "\n" +
                        "Valid from " + CreatedAt + " to " + ExpirationDate
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(fromEmail, appPassword);
                await client.SendAsync(_message);
                await client.DisconnectAsync(true);
            }
        }
    }
}