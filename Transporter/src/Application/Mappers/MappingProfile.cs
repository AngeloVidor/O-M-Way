using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.API.DTOs;
using src.Domain.Entities;

namespace src.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransporterCompany, TransporterCompanyDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();

        }
    }
}