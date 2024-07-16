using AutoMapper;
using DeskJr.Entity.Models;
using DeskJr.Service.Dto;
using DeskJr.Service.Dto.EmployeeDtos;
using DeskJr.Service.Dto.EmployeeTitleDtos;
using DeskJr.Service.Dto.TeamDtos;

namespace DeskJr.Service.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Team, UpdateTeamDto>().ReverseMap();
            CreateMap<Team, CreateTeamDto>().ReverseMap();
            CreateMap<EmployeeTitle, EmployeeTitleDto>().ReverseMap();
            CreateMap<EmployeeTitle, UpdateEmployeeTitleDto>().ReverseMap();
            CreateMap<EmployeeTitle, CreateEmployeeTitleDto>().ReverseMap();

        }
    }
}
