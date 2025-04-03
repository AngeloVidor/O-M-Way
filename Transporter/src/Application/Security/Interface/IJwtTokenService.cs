using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.API.DTOs;

namespace src.Application.Security.Interface
{
    public interface IJwtTokenService
    {
        Task<string> GenerateTokenAsync(string email, string role);
    }
}