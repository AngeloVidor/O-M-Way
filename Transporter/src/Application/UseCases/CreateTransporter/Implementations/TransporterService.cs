using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.Models;
using src.Application.UseCases.CheckZipCodeValidity.Interfaces;
using src.Application.UseCases.ConsultCNPJ.Interfaces;
using src.Application.UseCases.CreateTransporter.Interfaces;
using src.Application.UseCases.CreateTransporter.MethodResponse;
using src.Application.UseCases.GenerateVerificationCode.Interfaces;
using src.Application.UseCases.SendVerificationCodeToEmail.Interfaces;
using src.Domain.Entities;
using src.Infrastructure.Repositories.Interfaces;
using src.Infrastructure.Repositories.Interfaces.TemporaryData;
using src.Infrastructure.Repositories.Interfaces.VerificationCode;

namespace src.Application.UseCases.CreateTransporter.Implementations
{
    public class TransporterService : ITransporterService
    {
        private readonly IMapper _mapper;
        private readonly ITransporterRepository _transporterRepository;
        private readonly IVerificationCodeHandler _verificationCode;
        private readonly ISendVerificationCodeToEmailService _sendVerificationCodeToEmailService;
        private readonly IZipCodeValidityCheckerService _zipCodeValidityCheckerService;
        private readonly IConsultCnpjService _consultCnpjService;
        private readonly ITransporterTemporaryDataRepository _transporterTemporaryDataRepository;

        public TransporterService(IMapper mapper, ITransporterRepository transporterRepository, IVerificationCodeHandler verificationCode, ISendVerificationCodeToEmailService sendVerificationCodeToEmailService, IZipCodeValidityCheckerService zipCodeValidityCheckerService, IConsultCnpjService consultCnpjService, ITransporterTemporaryDataRepository transporterTemporaryDataRepository)
        {
            _mapper = mapper;
            _transporterRepository = transporterRepository;
            _verificationCode = verificationCode;
            _sendVerificationCodeToEmailService = sendVerificationCodeToEmailService;
            _zipCodeValidityCheckerService = zipCodeValidityCheckerService;
            _consultCnpjService = consultCnpjService;
            _transporterTemporaryDataRepository = transporterTemporaryDataRepository;
        }

        public async Task<PendingRegistration> StartRegistrationAsync(PendingRegistration pendingRegistration)
        {

            bool isValidCnpj = await _consultCnpjService.IsCnpjValidAsync(pendingRegistration.CNPJ);
            if (!isValidCnpj)
            {
                throw new ArgumentException("Invalid or inactive CNPJ. It must contain exactly 14 digits.");
            }

            bool isValidZipCode = await _zipCodeValidityCheckerService.IsValidZipCodeAsync(pendingRegistration.Location.CEP);
            if (!isValidZipCode)
            {
                throw new ArgumentException("Invalid zip code. It must contain exactly 8 digits without the hyphen.");
            }

            var response = await _verificationCode.GenerateCodeAsync(pendingRegistration.Email);
            pendingRegistration.VerificationCode = response.Code;

            await _sendVerificationCodeToEmailService.SentAsync(pendingRegistration.Email, response.Code, response.CreatedAt, response.ExpirationDate);

            pendingRegistration.Password = BCrypt.Net.BCrypt.HashPassword(pendingRegistration.Password);
            var temporaryData = await _transporterTemporaryDataRepository.AddTemporaryDataAsync(pendingRegistration);
            return temporaryData;
        }

        public async Task<RegistrationResult> EndRegistrationAsync(string verificationCode)
        {
            var code = await _verificationCode.GetVerificationCodeAsync(verificationCode);
            if (string.IsNullOrWhiteSpace(code.Code))
            {
                throw new ArgumentException("Invalid Verification Code");
            }
            DateTime currentTime = DateTime.Now;
            if (code.ExpirationDate <= currentTime)
            {
                throw new UnauthorizedAccessException("The code has expired and cannot be used.");
            }

            var temporaryData = await _transporterTemporaryDataRepository.GetTemporaryDataAsync(code.Code);


            var transporterCompany = new TransporterCompany
            {
                Name = temporaryData.Name,
                Email = temporaryData.Email,
                Password = temporaryData.Password,
                CNPJ = temporaryData.CNPJ,
                Phone = temporaryData.Phone,
                CreatedAt = DateTime.Now,
            };

            var result = await _transporterRepository.AddAsync(transporterCompany);
            var location = new Location
            {
                City = temporaryData.Location.City,
                State = temporaryData.Location.State,
                Country = temporaryData.Location.Country,
                Street = temporaryData.Location.Street,
                CEP = temporaryData.Location.CEP,
                Timestamp = DateTime.Now,
                Transporter_ID = result.Transporter_ID
            };
            var savedLocation = await _transporterRepository.AddLocationAsync(location);
            result.Location_ID = savedLocation.Location_ID;
            await _transporterRepository.UpdateAsync(result);

            return new RegistrationResult
            {
                Success = true,
                Message = "Registration completed successfully.",
            };

        }
    }
}