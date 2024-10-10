using AutoMapper;
using DeskJr.Entity.Models;
using DeskJr.Service.Dto;
using DeskJr.Service.Dto.LoggerDtos;


namespace DeskJr.Service.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, AddOrUpdateEmployeeDto>().ReverseMap();
            CreateMap<Employee, ManagerDto>().ReverseMap();
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Team, AddOrUpdateTeamDto>().ReverseMap();
            CreateMap<Team, CreateTeamDto>().ReverseMap();
            CreateMap<EmployeeTitle, EmployeeTitleDto>().ReverseMap();
            CreateMap<EmployeeTitle, UpdateEmployeeTitleDto>().ReverseMap();
            CreateMap<EmployeeTitle, CreateEmployeeTitleDto>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeCreateDTO>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeUpdateDTO>().ReverseMap();
            CreateMap<Leave, LeaveDTO>().ReverseMap();
            CreateMap<Leave, LeaveCreateDTO>().ReverseMap();
            CreateMap<Leave, LeaveUpdateDTO>().ReverseMap();
            CreateMap<Leave, PendingLeaveRequestDto>().ReverseMap();
            CreateMap<Leave, UpdateLeaveStatusDto>().ReverseMap();
            CreateMap<Holiday, HolidayDto>().ReverseMap();
            CreateMap<Holiday, AddOrUpdateHolidayDto>().ReverseMap();
            CreateMap<Setting, SettingDto>().ReverseMap();
            CreateMap<Setting, AddOrUpdateSettingDto>().ReverseMap();
            CreateMap<Survey, SurveyDto >().ReverseMap();
            CreateMap<Survey, AddOrUpdateSurveyDto >().ReverseMap();
            CreateMap<SurveyQuestion, SurveyQuestionDto>().ReverseMap();
            CreateMap<SurveyQuestion, AddOrUpdateServeyQuestionDto>().ReverseMap();
            CreateMap<SurveyQuestionOptions, SurveyQuestionOptionsDto>().ReverseMap();
            CreateMap<SurveyQuestionOptions, AddOrUpdateSurveyQuestionOptionsDto>().ReverseMap();
            CreateMap<EmployeeOptions, EmployeeOptionsDto>().ReverseMap();
            CreateMap<EmployeeOptions, CreateEmployeeOptionsDto>().ReverseMap();
            CreateMap<Log, LogDto>().ReverseMap();


        }
    }
}
