using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.Models;
using src.Application.UseCases.CreateTransporter.Interfaces;
using src.Application.UseCases.GenerateVerificationCode.Interfaces;
using src.Domain.Entities;
using src.Infrastructure.Repositories.Interfaces;
using src.Infrastructure.Repositories.Interfaces.VerificationCode;

namespace src.Application.UseCases.CreateTransporter.Implementations
{
    public class TransporterService : ITransporterService
    {
        private readonly IMapper _mapper;
        private readonly ITransporterRepository _transporterRepository;
        private readonly IVerificationCodeHandler _verificationCode;
        private readonly IVerificationCodeRepository _verificationCodeRepository;

        public TransporterService(IMapper mapper, ITransporterRepository transporterRepository, IVerificationCodeHandler verificationCode, IVerificationCodeRepository verificationCodeRepository)
        {
            _mapper = mapper;
            _transporterRepository = transporterRepository;
            _verificationCode = verificationCode;
            _verificationCodeRepository = verificationCodeRepository;
        }

        public async Task<bool> AddAsync(TransporterCompanyDTO transporterCompanyDTO)
        {
            var transporterEntity = _mapper.Map<TransporterCompany>(transporterCompanyDTO);

            var code = _verificationCode.GenerateCode(transporterCompanyDTO.Email).Result;
            var verificationCode = new VerificationCodeModel
            {
                Code = code,
                Email = transporterEntity.Email,
                CreatedAt = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMinutes(5)
            };
            await _verificationCodeRepository.SaveVerificationCodeAsync(verificationCode);
            Console.WriteLine($"Verification code: {code}");
            Console.WriteLine("Valid from " + verificationCode.CreatedAt + " to " + verificationCode.ExpirationDate);
            transporterEntity.VerificationCode_ID = verificationCode.VerificationCode_ID;

            var result = await _transporterRepository.AddAsync(transporterEntity);
            return result;
        }
    }
}