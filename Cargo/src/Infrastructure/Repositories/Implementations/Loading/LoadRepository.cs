using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.Loading;

namespace src.Infrastructure.Repositories.Implementations.Loading
{
    public class LoadRepository : ILoadRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LoadRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateLoadAsync(Load load)
        {
            await _dbContext.Loads.AddAsync(load);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;

        }
    }
}