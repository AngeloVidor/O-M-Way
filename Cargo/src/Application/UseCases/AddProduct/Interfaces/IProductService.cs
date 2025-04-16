using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;
using src.Application.Common;

namespace src.Application.UseCases.AddProduct.Interfaces
{
    public interface IProductService
    {
        Task<MethodResponse> AddProductToLoadAsync(ProductDTO product);

    }
}