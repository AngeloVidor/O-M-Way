using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces;

namespace src.Infrastructure.Repositories.Implementations
{
    public class TransporterRepository : ITransporterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransporterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(TransporterCompany transporter)
        {
            await _dbContext.Transporters.AddAsync(transporter);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}