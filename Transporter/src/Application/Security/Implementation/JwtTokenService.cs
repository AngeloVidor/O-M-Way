using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using src.API.DTOs;
using src.Application.Security.Interface;
using src.Application.Security.Model;
using src.Application.UseCases.Utility.Interfaces;

namespace src.Application.Security.Implementation
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtModel _jwtConfig;
        private readonly IUtilityService _utilityService;
        public JwtTokenService(JwtModel jwtConfig, IUtilityService utilityService)
        {
            _jwtConfig = jwtConfig;
            _utilityService = utilityService;
        }
        public async Task<string> GenerateTokenAsync(string email, string role)
        {
            var client = await _utilityService.GetTransporterByEmailAsync(email);
            if (client == null) throw new ArgumentException("Invalid email");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, client.Transporter_ID.ToString()),
                new Claim(ClaimTypes.Name, client.Name),
                new Claim(ClaimTypes.Role, role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.JWT_KEY));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtConfig.JWT_DurationInMinutes);

            var token = new JwtSecurityToken
            (
                issuer: _jwtConfig.JWT_ISSUER,
                audience: _jwtConfig.JWT_AUDIENCE,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}