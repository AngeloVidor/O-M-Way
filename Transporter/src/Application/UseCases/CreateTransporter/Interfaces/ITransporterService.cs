using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;
using src.Application.UseCases.CreateTransporter.MethodResponse;
using src.Domain.Entities;

namespace src.Application.UseCases.CreateTransporter.Interfaces
{
    public interface ITransporterService
    {
        Task<PendingRegistration> StartRegistrationAsync(PendingRegistration pendingRegistration);
        Task<RegistrationResult> EndRegistrationAsync(string verificationCode);
    }
}