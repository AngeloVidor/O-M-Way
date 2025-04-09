using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;

namespace src.Application.UseCases.Employees.Interfaces
{
    public interface IEmployeerManagerService
    {
        Task<EmployeeDTO> FindDriverInTransporterAsync(long transporterId, long driverId);
    }
}