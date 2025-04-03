using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Application.UseCases.AuthenticateTransporter.Interfaces;

namespace src.API.Controllers.Authenticate
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateTransporterService _authenticateTransporterService;

        public AuthenticateController(IAuthenticateTransporterService authenticateTransporterService)
        {
            _authenticateTransporterService = authenticateTransporterService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var result = await _authenticateTransporterService.LoginAsync(email, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}