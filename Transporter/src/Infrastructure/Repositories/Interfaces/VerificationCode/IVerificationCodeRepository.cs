using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.Models;

namespace src.Infrastructure.Repositories.Interfaces.VerificationCode
{
    public interface IVerificationCodeRepository
    {
        Task<bool> SaveVerificationCodeAsync(VerificationCodeModel verificationCodeModel);
        Task<VerificationCodeModel> GetVerificationCodeAsync(string verificationCode);
    }
}