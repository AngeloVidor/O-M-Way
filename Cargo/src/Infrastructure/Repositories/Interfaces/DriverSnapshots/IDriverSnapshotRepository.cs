using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Infrastructure.Repositories.Interfaces.DriverSnapshots
{
    public interface IDriverSnapshotRepository
    {
        Task<bool> SaveCopyAsync(DriverSnapshot snapshot);
        Task<DriverSnapshot> GetTransporterDriverAsync(long transporterId, long driverId);
    }
}