using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.API.DTOs;
using src.Application.UseCases.CreateTransporter.Interfaces;
using src.Domain.Entities;

namespace src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ITransporterService _transporterService;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ITransporterService transporterService, ILogger<RegistrationController> logger)
        {
            _transporterService = transporterService;
            _logger = logger;
        }

        [HttpPost("StartRegistration")]
        public async Task<IActionResult> AddAsync([FromBody] PendingRegistration pendingRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                _logger.LogInformation("Starting registration process for email: {Email}", pendingRegistration.Email);
                var result = await _transporterService.StartRegistrationAsync(pendingRegistration);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("EndRegistration")]
        public async Task<IActionResult> EndRegistration(string verificationCode)
        {
            try
            {
                var result = await _transporterService.EndRegistrationAsync(verificationCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}