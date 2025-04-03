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
    public class TransporterController : ControllerBase
    {
        private readonly ITransporterService _transporterService;

        public TransporterController(ITransporterService transporterService)
        {
            _transporterService = transporterService;
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