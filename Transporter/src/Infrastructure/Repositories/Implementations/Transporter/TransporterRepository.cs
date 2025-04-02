using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task<TransporterCompany> AddAsync(TransporterCompany transporter)
        {
            await _dbContext.Transporters.AddAsync(transporter);
            await _dbContext.SaveChangesAsync();
            return transporter;
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            await _dbContext.Locations.AddAsync(location);
            await _dbContext.SaveChangesAsync();
            return location;
        }

        public async Task UpdateAsync(TransporterCompany transporter)
        {
            _dbContext.Transporters.Update(transporter);
            await _dbContext.SaveChangesAsync();
        }

    }
}