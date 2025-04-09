using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.Common;
using src.Application.UseCases.CreateLoad.Interfaces;
using src.Domain.Entities;
using src.Infrastructure.Broker.Driver.Interface;
using src.Infrastructure.Repositories.Interfaces.Loading;

namespace src.Application.UseCases.CreateLoad.Implementations
{
    public class LoadService : ILoadService
    {
        private readonly ILoadRepository _loadRepository;
        private readonly IMapper _mapper;
        private readonly IDriverIdentificationPublisher _publisher;

        public LoadService(ILoadRepository loadRepository, IMapper mapper, IDriverIdentificationPublisher publisher)
        {
            _loadRepository = loadRepository;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<MethodResponse> CreateLoadAsync(LoadDTO load)
        {
            var loadEntity = _mapper.Map<Load>(load);
            
            await _publisher.PublishAsync(loadEntity.Transporter_ID, loadEntity.Driver_ID);
            try
            {

                var result = await _loadRepository.CreateLoadAsync(loadEntity);
                if (result)
                {
                    return new MethodResponse
                    {
                        Success = true,
                        Message = "Load created successfully."
                    };
                }
                return new MethodResponse
                {
                    Success = false,
                    Message = "Failed to create load."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Load creation failed. Message: {ex.Message} | StackTrace: {ex.StackTrace}");

                return new MethodResponse
                {
                    Success = false,
                    Message = "Failed to create load."
                };
            }

        }
    }
}