using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.Employees;

namespace src.Infrastructure.Repositories.Implementations.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}