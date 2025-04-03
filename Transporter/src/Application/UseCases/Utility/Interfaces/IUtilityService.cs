using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;

namespace src.Application.UseCases.Utility.Interfaces
{
    public interface IUtilityService
    {
        Task<TransporterCompanyDTO> GetTransporterByEmailAsync(string email);
    }
}