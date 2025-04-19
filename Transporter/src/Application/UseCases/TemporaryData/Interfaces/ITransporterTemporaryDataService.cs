using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Application.UseCases.TemporaryData.Interfaces
{
    public interface ITransporterTemporaryDataService
    {
        Task<PendingRegistration> UpdateCnpjValuesAsync(long transporterId, bool isValid);
        Task<bool> UpdateVerificationCodeAsync(long transporterId, string verificationCode);
    }
}