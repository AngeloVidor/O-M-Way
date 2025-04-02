using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.TemporaryData;

namespace src.Infrastructure.Repositories.Implementations.TemporaryData
{
    public class TransporterTemporaryDataRepository : ITransporterTemporaryDataRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransporterTemporaryDataRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PendingRegistration> AddTemporaryDataAsync(PendingRegistration pendingRegistration)
        {
            await _dbContext.TransporterPreRegistrations.AddAsync(pendingRegistration);
            await _dbContext.SaveChangesAsync();
            return pendingRegistration;
        }

        public async Task<PendingRegistration> GetTemporaryDataAsync(string verificationCode)
        {
            return await _dbContext.TransporterPreRegistrations
                .Include(p => p.Location)
                .FirstOrDefaultAsync(x => x.VerificationCode == verificationCode);
        }
    }
}