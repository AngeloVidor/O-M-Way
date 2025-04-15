using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.Loading.Management;

namespace src.Infrastructure.Repositories.Implementations.Loading.Management
{
    public class LoadManagementRepository : ILoadManagementRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LoadManagementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Load> GetLoadByIdAsync(long loadId)
        {
            return await _dbContext.Loads.AsNoTracking().FirstOrDefaultAsync(x => x.Load_ID == loadId);
        }
    }
}