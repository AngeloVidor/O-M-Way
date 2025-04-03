using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using src.Application.Security.Model;

namespace src.API.Middlewares
{
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtModel _jwtConfig;

        public JwtAuthorizationMiddleware(RequestDelegate next, JwtModel jwtConfig)
        {
            _next = next;
            _jwtConfig = jwtConfig;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            List<string> PublicRoutes = new List<string>
            {
                "/api/Authenticate/login",
                "/api/Transporter/StartRegistration",
                "/api/Transporter/EndRegistration"
            };

            if (PublicRoutes.Contains(context.Request.Path.Value, StringComparer.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            var tokenAccess = context.Request.Headers["Authorization"].FirstOrDefault().Split("")[1];
            if (string.IsNullOrEmpty(tokenAccess)) throw new Exception("Token not provided");


            var isValid = ValidateToken(tokenAccess);
            if (!isValid) throw new Exception("Invalid token");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(tokenAccess) as JwtSecurityToken;
            var transporterIdClaims = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (transporterIdClaims == null) throw new Exception("TransporterID not found in claims");

            context.Items["transporterId"] = transporterIdClaims.Value;
            await _next(context);
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var secretKey = _jwtConfig.JWT_KEY;
                if (string.IsNullOrEmpty(secretKey)) throw new Exception("JWT Secret Key is not configured");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _jwtConfig.JWT_ISSUER,
                    ValidAudience = _jwtConfig.JWT_AUDIENCE,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (!(validatedToken is JwtSecurityToken jwtSecurityToken)) throw new Exception("Invalid token");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Token validation failed: {ex.Message}");
            }
        }

    }
}