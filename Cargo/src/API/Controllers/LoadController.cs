using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using src.API.DTOs;
using src.Application.UseCases.CreateLoad.Interfaces;
using static src.Domain.Entities.Load;

namespace src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoadController : ControllerBase
    {
        private readonly ILoadService _loadService;

        public LoadController(ILoadService loadService)
        {
            _loadService = loadService;
        }

        [HttpPost("driver/{driverId}/deadline/{deliveryDeadline}")]
        [Authorize(Roles = "transporter")]
        public async Task<IActionResult> CreateLoad(long driverId, DateTime deliveryDeadline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            long transporterId = long.Parse(HttpContext.Items["transporterId"].ToString());
            var load = new LoadDTO
            {
                Transporter_ID = transporterId,
                Driver_ID = driverId,
                LoadingStartedAt = DateTime.Now,
                DeliveryDeadline = deliveryDeadline,
                IsDelivered = false,
                Status = LoadStatus.Created
            };

            try
            {
                var response = await _loadService.CreateLoadAsync(load);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(new { message = response.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}


