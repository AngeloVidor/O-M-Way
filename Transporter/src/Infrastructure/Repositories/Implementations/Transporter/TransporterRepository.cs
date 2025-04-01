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

        public async Task<bool> AddAsync(TransporterCompany transporter)
        {
            await _dbContext.Transporters.AddAsync(transporter);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task UpdateLocationAsync(Location location)
        {
            var existingLocation = await _dbContext.Locations.FindAsync(location.Location_ID);
            if (existingLocation != null)
            {
                existingLocation.Location_ID = location.Location_ID;
                existingLocation.City = location.City;
                existingLocation.State = location.State;
                existingLocation.Country = location.Country;
                existingLocation.Street = location.Street;
                existingLocation.CEP = location.CEP;
                existingLocation.Timestamp = location.Timestamp;
                existingLocation.Transporter_ID = location.Transporter_ID;

                _dbContext.Locations.Update(existingLocation);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Location not found");
            }
        }
    }
}