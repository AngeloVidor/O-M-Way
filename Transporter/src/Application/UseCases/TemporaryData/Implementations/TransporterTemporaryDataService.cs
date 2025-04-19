using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.UseCases.TemporaryData.Interfaces;
using src.Domain.Entities;
using src.Infrastructure.Repositories.Interfaces.TemporaryData;

namespace src.Application.UseCases.TemporaryData.Implementations
{
    public class TransporterTemporaryDataService : ITransporterTemporaryDataService
    {
        private readonly ITransporterTemporaryDataRepository _transporterTemporaryDataRepository;

        public TransporterTemporaryDataService(ITransporterTemporaryDataRepository transporterTemporaryDataRepository)
        {
            _transporterTemporaryDataRepository = transporterTemporaryDataRepository;
        }

        public async Task<PendingRegistration> UpdateCnpjValuesAsync(long transporterId, bool isValid)
        {
            return await _transporterTemporaryDataRepository.UpdateCnpjValuesAsync(transporterId, isValid);
        }

        public async Task<bool> UpdateVerificationCodeAsync(long transporterId, string verificationCode)
        {
            return await _transporterTemporaryDataRepository.UpdateVerificationCodeAsync(transporterId, verificationCode);
        }
    }
}