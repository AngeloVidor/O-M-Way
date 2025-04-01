using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;

namespace src.Application.UseCases.CreateTransporter.Interfaces
{
    public interface ITransporterService
    {
        Task<bool> AddAsync(TransporterCompanyDTO transporterCompanyDTO);
    }
}