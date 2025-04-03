using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Application.Common;
using src.Application.UseCases.AuthenticateTransporter.Interfaces;
using src.Infrastructure.Repositories.Interfaces.Utility;

namespace src.Application.UseCases.AuthenticateTransporter.Implementations
{
    public class AuthenticateTransporterService : IAuthenticateTransporterService
    {
        private readonly IUtilityRepository _utilityRepository;

        public AuthenticateTransporterService(IUtilityRepository utilityRepository)
        {
            _utilityRepository = utilityRepository;
        }

        public async Task<MethodResponse> LoginAsync(string email, string password)
        {
            var transporter = await _utilityRepository.GetTransporterByEmailAsync(email);
            if (transporter == null || !BCrypt.Net.BCrypt.Verify(password, transporter.Password))
            {
                return new MethodResponse
                {
                    Success = false,
                    Message = "Invallid e-mail or password."
                };
            }
            return new MethodResponse
            {
                Success = true,
                Message = "Login successful."
            };
        }
    }
}