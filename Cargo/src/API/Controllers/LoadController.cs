using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("create")]
        [Authorize(Roles = "transporter")]
        public async Task<IActionResult> CreateLoad(long driverId)
        {

            long transporterId = long.Parse(HttpContext.Items["transporterId"].ToString());
            var load = new LoadDTO
            {
                Transporter_ID = transporterId,
                Driver_ID = driverId,
                LoadingStartedAt = DateTime.Now,
                DeliveryDeadline = DateTime.Now.AddDays(7),
                IsDelivered = false,
                Status = LoadStatus.Created
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _loadService.CreateLoadAsync(load);
                return Ok(load);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}