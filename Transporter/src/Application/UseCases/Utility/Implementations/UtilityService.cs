using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.UseCases.Utility.Interfaces;
using src.Infrastructure.Repositories.Interfaces.Utility;

namespace src.Application.UseCases.Utility.Implementations
{
    public class UtilityService : IUtilityService
    {
        private readonly IUtilityRepository _utilityRepository;
        private readonly IMapper _mapper;

        public UtilityService(IUtilityRepository utilityRepository, IMapper mapper)
        {
            _utilityRepository = utilityRepository;
            _mapper = mapper;
        }

        public async Task<TransporterCompanyDTO> GetTransporterByEmailAsync(string email)
        {
            var transporter = await _utilityRepository.GetTransporterByEmailAsync(email);
            if (transporter == null) throw new ArgumentException("Invalid email");
            
            return _mapper.Map<TransporterCompanyDTO>(transporter);
        }
    }
}