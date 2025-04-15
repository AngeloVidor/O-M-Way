using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;
using src.Infrastructure.Data;
using src.Infrastructure.Repositories.Interfaces.Loading;

namespace src.Infrastructure.Repositories.Implementations.Items
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddLoadItemAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}