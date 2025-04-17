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
        private readonly ILogger<UtilityRepository> _logger;


        public UtilityRepository(ApplicationDbContext dbContext, ILogger<UtilityRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            try
            {
                var isEmailAvailable = await _dbContext.Transporters.FirstOrDefaultAsync(x => x.Email == email);
                if (isEmailAvailable != null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking the email availability.");
                throw new Exception("An error occurred while checking the email availability.", ex);
            }
        }

        public async Task<TransporterCompany> GetTransporterByEmailAsync(string email)
        {
            return await _dbContext.Transporters.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}