using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;
using src.Application.Common;
using src.Domain.Entities;

namespace src.Application.UseCases.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<MethodResponse> AddEmployeeAsync(EmployeeDTO employee);
    }
}