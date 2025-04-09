using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Infrastructure.Repositories.Interfaces.Employees
{
    public interface IEmployeerManagerRepository
    {
        Task<bool> GetEmployeerByUsernameAsync(string username);
        Task<Employee> FindDriverInTransporterAsync(long transporterId, long driverId);
    }
}