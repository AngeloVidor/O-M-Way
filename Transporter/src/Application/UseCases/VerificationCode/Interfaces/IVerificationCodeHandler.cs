using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.Models;

namespace src.Application.UseCases.GenerateVerificationCode.Interfaces
{
    public interface IVerificationCodeHandler
    {
        Task<VerificationCodeModel> GenerateCodeAsync(string email);
        Task<VerificationCodeModel> GetVerificationCodeAsync(string verificationCode);

    }
}