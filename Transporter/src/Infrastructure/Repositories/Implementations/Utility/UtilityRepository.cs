using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.Utility;

namespace src.Infrastructure.Repositories.Implementations.Utility
{
    public class UtilityRepository : IUtilityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UtilityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            var isEmailAvailable = await _dbContext.Transporters.FirstOrDefaultAsync(x => x.Email == email);
            if (isEmailAvailable != null)
            {
                return false;
            }
            return true;
        }

        public async Task<TransporterCompany> GetTransporterByEmailAsync(string email)
        {
            return await _dbContext.Transporters.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}