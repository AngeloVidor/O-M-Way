using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Infrastructure.Repositories.Interfaces.Loading
{
    public interface IProductRepository
    {
        Task<bool> AddLoadItemAsync(Product product);
    }
}