using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Application.UseCases.SendVerificationCodeToEmail.Interfaces
{
    public interface ISendVerificationCodeToEmailService
    {
        Task SentAsync(string email, string code, DateTime CreatedAt, DateTime ExpirationDate);
    }
}