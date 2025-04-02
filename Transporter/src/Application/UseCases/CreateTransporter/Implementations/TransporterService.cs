using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.Models;
using src.Application.UseCases.CheckZipCodeValidity.Interfaces;
using src.Application.UseCases.CreateTransporter.Interfaces;
using src.Application.UseCases.GenerateVerificationCode.Interfaces;
using src.Application.UseCases.SendVerificationCodeToEmail.Interfaces;
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
        private readonly ISendVerificationCodeToEmailService _sendVerificationCodeToEmailService;
        private readonly IZipCodeValidityCheckerService _zipCodeValidityCheckerService;

        public TransporterService(IMapper mapper, ITransporterRepository transporterRepository, IVerificationCodeHandler verificationCode, IVerificationCodeRepository verificationCodeRepository, ISendVerificationCodeToEmailService sendVerificationCodeToEmailService, IZipCodeValidityCheckerService zipCodeValidityCheckerService)
        {
            _mapper = mapper;
            _transporterRepository = transporterRepository;
            _verificationCode = verificationCode;
            _verificationCodeRepository = verificationCodeRepository;
            _sendVerificationCodeToEmailService = sendVerificationCodeToEmailService;
            _zipCodeValidityCheckerService = zipCodeValidityCheckerService;
        }

        public async Task<bool> AddAsync(TransporterCompanyDTO transporterCompanyDTO)
        {
            var transporterEntity = _mapper.Map<TransporterCompany>(transporterCompanyDTO);

            bool isValidZipCode = await _zipCodeValidityCheckerService.IsValidZipCodeAsync(transporterEntity.Location.CEP);
            if (!isValidZipCode)
            {
                throw new ArgumentException("Invalid zip code. It must contain exactly 8 digits without the hyphen.");
            }

            var code = _verificationCode.GenerateCode(transporterCompanyDTO.Email).Result;
            var verificationCode = new VerificationCodeModel
            {
                Code = code,
                Email = transporterEntity.Email,
                CreatedAt = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5)
            };
            transporterEntity.Location.Transporter_ID = transporterCompanyDTO.Transporter_ID;
            await _verificationCodeRepository.SaveVerificationCodeAsync(verificationCode);
            transporterEntity.VerificationCode_ID = verificationCode.VerificationCode_ID;
            await _sendVerificationCodeToEmailService.SentAsync(transporterEntity.Email, verificationCode.Code, verificationCode.CreatedAt, verificationCode.ExpirationDate);

            var result = await _transporterRepository.AddAsync(transporterEntity);
            if (result)
            {
                transporterEntity.Location.Transporter_ID = transporterEntity.Transporter_ID;
                await _transporterRepository.UpdateLocationAsync(transporterEntity.Location);
            }
            return result;
        }
    }
}