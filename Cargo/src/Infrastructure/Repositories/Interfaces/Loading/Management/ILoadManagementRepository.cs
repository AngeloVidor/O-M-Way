using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Infrastructure.Repositories.Interfaces.Loading.Management
{
    public interface ILoadManagementRepository
    {
        Task<Load> GetLoadByIdAsync(long loadId);
    }
}