using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Application.Common;
using src.Application.UseCases.Employees.Interfaces;
using src.Domain.Entities;
using src.Infrastructure.Broker.Events.DriverCreated.Interfaces;
using src.Infrastructure.Repositories.Interfaces.Employees;

namespace src.Application.UseCases.Employees.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeerManagerRepository _employeerManagerRepository;
        private readonly IDriverEventPublisher _driverEventPublisher;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository, IEmployeerManagerRepository employeerManagerRepository, IDriverEventPublisher driverEventPublisher)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _employeerManagerRepository = employeerManagerRepository;
            _driverEventPublisher = driverEventPublisher;
        }

        public async Task<MethodResponse> AddEmployeeAsync(EmployeeDTO employee)
        {
            var employeeEntity = _mapper.Map<Employee>(employee);

            var isUsernameTaken = await _employeerManagerRepository.GetEmployeerByUsernameAsync(employee.Username);
            if (isUsernameTaken)
            {
                return new MethodResponse
                {
                    Success = false,
                    Message = "Username is already taken"
                };
            }

            employeeEntity.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
            var addedEmployee = await _employeeRepository.AddEmployeeAsync(employeeEntity);
            _driverEventPublisher.Publish(employee.Transporter_ID, employeeEntity.Employee_ID, employee.Username).Wait();

            return new MethodResponse
            {
                Success = addedEmployee,
                Message = addedEmployee ? "Employee added successfully" : "Failed to add employee"
            };
        }
    }
}