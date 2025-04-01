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
                throw new InvalidOperationException("O endereço de e-mail do remetente não está configurado.");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException("O e-mail do destinatário não pode ser nulo ou vazio.");
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