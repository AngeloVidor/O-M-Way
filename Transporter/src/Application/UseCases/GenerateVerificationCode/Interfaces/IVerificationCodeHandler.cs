using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Application.UseCases.GenerateVerificationCode.Interfaces
{
    public interface IVerificationCodeHandler
    {
        Task<string> GenerateCode(string email);

    }
}