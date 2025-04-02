using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Infrastructure.Repositories.Interfaces
{
    public interface ITransporterRepository
    {
        Task<TransporterCompany> AddAsync(TransporterCompany transporter);
        Task<Location> AddLocationAsync(Location location);
        Task UpdateAsync(TransporterCompany transporter);
    }
}