using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.UseCases.GenerateVerificationCode.Interfaces;

namespace src.Application.UseCases.GenerateVerificationCode.Implementations
{
    public class VerificationCodeHandler : IVerificationCodeHandler
    {
        public Task<string> GenerateCode(string email)
        {
            var random = new Random();
            string code = random.Next(100000, 999999).ToString();
            return Task.FromResult(code);
        }
    }
}