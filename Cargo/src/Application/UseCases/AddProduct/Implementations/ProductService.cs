using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.Common;
using src.Application.UseCases.AddProduct.Interfaces;
using src.Application.UseCases.CheckZipCodeValidity.Interfaces;
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
        private readonly IZipCodeValidityCheckerService _zipCodeValidityCheckerService;

        public ProductService(IProductRepository productRepository, IMapper mapper, IValidateCpfService validateCpfService, ILoadManagementRepository loadManagementRepository, IZipCodeValidityCheckerService zipCodeValidityCheckerService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validateCpfService = validateCpfService;
            _loadManagementRepository = loadManagementRepository;
            _zipCodeValidityCheckerService = zipCodeValidityCheckerService;
        }

        public async Task<MethodResponse> AddProductToLoadAsync(ProductDTO product)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(product);
                var isValid = _validateCpfService.IsValid(productEntity.RecipientCPF);
                if (!isValid)
                {
                    return new MethodResponse
                    {
                        Success = false,
                        Message = "Invalid CPF"
                    };
                }
                Console.WriteLine($"ZipCode: {productEntity.Address.ZipCode}");
                bool isZipCodeValid = await _zipCodeValidityCheckerService.IsValidZipCodeAsync(productEntity.Address.ZipCode);
                if (!isZipCodeValid)
                {
                    return new MethodResponse
                    {
                        Success = false,
                        Message = "Invalid ZipCode"
                    };
                }

                var load = await _loadManagementRepository.GetLoadByIdAsync(productEntity.Load_ID);
                if (load == null)
                {
                    return new MethodResponse
                    {
                        Success = false,
                        Message = "Load not found"
                    };
                }
                await _productRepository.AddLoadItemAsync(productEntity);
                return new MethodResponse
                {
                    Success = true,
                    Message = "Product added to load successfully"
                };
            }
            catch (Exception ex)
            {
                return new MethodResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }


        }
    }
}