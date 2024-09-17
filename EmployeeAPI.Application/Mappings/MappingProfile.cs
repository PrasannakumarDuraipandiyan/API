using AutoMapper;
using EmployeeAPI.Application.DTOs;
using EmployeeAPI.Domain.Entities;

namespace EmployeeAPI.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
    }
}