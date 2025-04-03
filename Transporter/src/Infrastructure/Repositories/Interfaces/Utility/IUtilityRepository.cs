using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Infrastructure.Repositories.Interfaces.Utility
{
    public interface IUtilityRepository
    {
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<TransporterCompany> GetTransporterByEmailAsync(string email);
    }
}