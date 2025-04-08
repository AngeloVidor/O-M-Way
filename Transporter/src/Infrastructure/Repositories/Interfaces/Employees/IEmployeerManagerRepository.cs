using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Infrastructure.Repositories.Interfaces.Employees
{
    public interface IEmployeerManagerRepository
    {
        Task<bool> GetEmployeerByUsernameAsync(string username);
    }
}