using AutoMapper;
using DeskJr.Entity.Models;
using DeskJr.Service.Dto;
using DeskJr.Service.Dto.EmployeeDtos;

namespace DeskJr.Service.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        }
    }
}
