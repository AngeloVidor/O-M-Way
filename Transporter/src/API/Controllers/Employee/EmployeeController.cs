using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.API.DTOs;
using src.Application.UseCases.Employees.Interfaces;

namespace src.API.Controllers.Employee
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("employee")]
        [Authorize(Roles = "transporter")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDTO employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                employee.Transporter_ID = long.Parse(HttpContext.Items["transporterId"].ToString());
                var result = await _employeeService.AddEmployeeAsync(employee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }
    }
}