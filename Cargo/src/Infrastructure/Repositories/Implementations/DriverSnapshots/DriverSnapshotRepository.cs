using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.DriverSnapshots;

namespace src.Infrastructure.Repositories.Implementations.DriverSnapshots
{
    public class DriverSnapshotRepository : IDriverSnapshotRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DriverSnapshotRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveCopyAsync(DriverSnapshot snapshot)
        {
            await _dbContext.DriverSnapshots.AddAsync(snapshot);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<DriverSnapshot> GetTransporterDriverAsync(long transporterId, long driverId)
        {
            return await _dbContext.DriverSnapshots.AsNoTracking().FirstOrDefaultAsync(x => x.Transporter_ID == transporterId && x.Employee_ID == driverId);
        }
    }
}