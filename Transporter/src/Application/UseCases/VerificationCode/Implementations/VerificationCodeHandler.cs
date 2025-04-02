using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.Models;
using src.Application.UseCases.GenerateVerificationCode.Interfaces;
using src.Infrastructure.Repositories.Interfaces.VerificationCode;

namespace src.Application.UseCases.GenerateVerificationCode.Implementations
{
    public class VerificationCodeHandler : IVerificationCodeHandler
    {
        private readonly IVerificationCodeRepository _verificationCodeRepository;

        public VerificationCodeHandler(IVerificationCodeRepository verificationCodeRepository)
        {
            _verificationCodeRepository = verificationCodeRepository;
        }

        public async Task<VerificationCodeModel> GenerateCodeAsync(string email)
        {
            var random = new Random();
            string code = random.Next(100000, 999999).ToString();
            var verificationCode = new VerificationCodeModel
            {
                Code = code,
                Email = email,
                CreatedAt = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMinutes(5)
            };
            await _verificationCodeRepository.SaveVerificationCodeAsync(verificationCode);
            return verificationCode;
        }

        public async Task<VerificationCodeModel> GetVerificationCodeAsync(string verificationCode)
        {
            return await _verificationCodeRepository.GetVerificationCodeAsync(verificationCode);
        }
    }
}