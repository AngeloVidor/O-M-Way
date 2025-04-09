using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Org.BouncyCastle.Crypto.Modes;
using src.API.DTOs;
using src.Application.Common;
using src.Application.UseCases.Employees.Interfaces;
using src.Infrastructure.Repositories.Interfaces.Employees;

namespace src.Application.UseCases.Employees.Implementations
{
    public class EmployeerManagerService : IEmployeerManagerService
    {
        private readonly IEmployeerManagerRepository _employeerManagerRepository;

        public EmployeerManagerService(IEmployeerManagerRepository employeerManagerRepository)
        {
            _employeerManagerRepository = employeerManagerRepository;
        }

        public async Task<EmployeeDTO> FindDriverInTransporterAsync(long transporterId, long driverId)
        {
            var driver = await _employeerManagerRepository.FindDriverInTransporterAsync(transporterId, driverId);
            if (driver != null)
            {
                return new EmployeeDTO
                {
                    Employee_ID = driver.Employee_ID,
                    Username = driver.Username,
                    Transporter_ID = driver.Transporter_ID,
                };
            }
            return null;
        }
    }
}