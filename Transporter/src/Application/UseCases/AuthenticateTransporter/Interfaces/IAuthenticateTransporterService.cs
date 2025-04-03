using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.Common;

namespace src.Application.UseCases.AuthenticateTransporter.Interfaces
{
    public interface IAuthenticateTransporterService
    {
        Task<MethodResponse> LoginAsync(string email, string password);
    }
}