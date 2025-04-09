using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.Employees;

namespace src.Infrastructure.Repositories.Implementations.Employees
{
    public class EmployeerManagerRepository : IEmployeerManagerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeerManagerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetEmployeerByUsernameAsync(string username)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Username == username) != null;
        }

        public async Task<Employee> FindDriverInTransporterAsync(long transporterId, long driverId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Transporter_ID == transporterId && x.Employee_ID == driverId);
        }
    }
}