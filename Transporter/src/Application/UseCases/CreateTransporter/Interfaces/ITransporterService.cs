using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;
using src.Application.Common;
using src.Domain.Entities;

namespace src.Application.UseCases.CreateTransporter.Interfaces
{
    public interface ITransporterService
    {
        Task<PendingRegistration> StartRegistrationAsync(PendingRegistration pendingRegistration);
        Task<MethodResponse> EndRegistrationAsync(string verificationCode);
    }
}