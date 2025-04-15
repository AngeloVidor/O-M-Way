using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.Common;
using src.Application.UseCases.CreateLoad.Interfaces;
using src.Application.UseCases.ValidateCPF.Interface;
using src.Domain.Entities;
using src.Infrastructure.Repositories.Interfaces.DriverSnapshots;
using src.Infrastructure.Repositories.Interfaces.Loading;

namespace src.Application.UseCases.CreateLoad.Implementations
{
    public class LoadService : ILoadService
    {
        private readonly ILoadRepository _loadRepository;
        private readonly IMapper _mapper;
        private readonly IDriverSnapshotRepository _driverSnapshotRepository;

        public LoadService(ILoadRepository loadRepository, IMapper mapper, IDriverSnapshotRepository driverSnapshotRepository)
        {
            _loadRepository = loadRepository;
            _mapper = mapper;
            _driverSnapshotRepository = driverSnapshotRepository;
        }

        public async Task<MethodResponse> CreateLoadAsync(LoadDTO load)
        {
            var loadEntity = _mapper.Map<Load>(load);

            var driver = await _driverSnapshotRepository.GetTransporterDriverAsync(loadEntity.Transporter_ID, loadEntity.Driver_ID);
            if (driver == null)
            {
                return new MethodResponse
                {
                    Success = false,
                    Message = "Driver not found for the transporter."
                };
            }
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
                    Message = ex.Message
                };
            }

        }
    }
}