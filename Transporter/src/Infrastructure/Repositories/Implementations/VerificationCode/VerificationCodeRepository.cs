using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Application.Models;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.VerificationCode;

namespace src.Infrastructure.Repositories.Implementations.VerificationCode
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VerificationCodeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VerificationCodeModel> GetVerificationCodeAsync(string verificationCode)
        {
            var codeObject = await _dbContext.VerificationCodes.FirstOrDefaultAsync(x => x.Code == verificationCode);
            return codeObject;
        }

        public async Task<bool> SaveVerificationCodeAsync(VerificationCodeModel verificationCodeModel)
        {
            await _dbContext.VerificationCodes.AddAsync(verificationCodeModel);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}