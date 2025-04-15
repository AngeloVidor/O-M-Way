using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.Common;
using src.Application.UseCases.AddProduct.Interfaces;
using src.Application.UseCases.ValidateCPF.Interface;
using src.Domain.Entities;
using src.Infrastructure.Repositories.Interfaces.Loading;
using src.Infrastructure.Repositories.Interfaces.Loading.Management;
using static src.Domain.Entities.Product;

namespace src.Application.UseCases.AddProduct.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidateCpfService _validateCpfService;
        private readonly ILoadManagementRepository _loadManagementRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper, IValidateCpfService validateCpfService, ILoadManagementRepository loadManagementRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validateCpfService = validateCpfService;
            _loadManagementRepository = loadManagementRepository;
        }

        public async Task<MethodResponse> AddLoadItemAsync(ProductDTO product)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                var isValid = _validateCpfService.IsValid(productEntity.RecipientCPF);
                if (!isValid)
                {
                    var response = new MethodResponse
                    {
                        Success = false,
                        Message = "Invalid CPF"
                    };
                }
                var load = await _loadManagementRepository.GetLoadByIdAsync(productEntity.Load_ID);
                if (load == null)
                {

                }
            }
            catch (Exception ex)
            {

            }
            throw new NotImplementedException();
        }
    }
}