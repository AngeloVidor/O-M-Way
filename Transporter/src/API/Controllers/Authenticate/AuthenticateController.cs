using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.API.DTOs;
using src.Application.Security.Interface;
using src.Application.UseCases.AuthenticateTransporter.Interfaces;

namespace src.API.Controllers.Authenticate
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateTransporterService _authenticateTransporterService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticateController(IAuthenticateTransporterService authenticateTransporterService, IJwtTokenService jwtTokenService)
        {
            _authenticateTransporterService = authenticateTransporterService;
            _jwtTokenService = jwtTokenService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            string role = "transporter";
            try
            {
                var result = await _authenticateTransporterService.LoginAsync(email, password);
                var token = await _jwtTokenService.GenerateTokenAsync(email, role);
                return Ok(new { Token = token, User = result });
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}