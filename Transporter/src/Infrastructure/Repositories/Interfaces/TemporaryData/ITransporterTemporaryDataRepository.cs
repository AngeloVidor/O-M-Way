using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Domain.Entities;

namespace src.Infrastructure.Repositories.Interfaces.TemporaryData
{
    public interface ITransporterTemporaryDataRepository
    {
        Task<PendingRegistration> AddTemporaryDataAsync(PendingRegistration pendingRegistration);
        Task<PendingRegistration> GetTemporaryDataAsync(string verificationCode);
        Task<PendingRegistration> UpdateCnpjValuesAsync(long transporterId, bool isValid);
        Task<bool> UpdateVerificationCodeAsync(long transporterId, string verificationCode);
    }
}