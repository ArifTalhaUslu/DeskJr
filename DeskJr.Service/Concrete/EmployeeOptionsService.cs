using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Repository.Concrete;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class EmployeeOptionsService : IEmployeeOptionsService
    {
        private readonly IEmployeeOptionsRepository _employeeOptionRepository;
        private readonly IMapper _mapper;
        public EmployeeOptionsService(IEmployeeOptionsRepository employeeOptionsRepository, IMapper mapper)
        {
            _employeeOptionRepository = employeeOptionsRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddOrUpdateEmployeeOptionsAsync(CreateEmployeeOptionsDto employeeOptions)
        {

            if (employeeOptions.Id == null)
            {
                return await _employeeOptionRepository.AddAsync(_mapper.Map<EmployeeOptions>(employeeOptions));
            }

            return await _employeeOptionRepository.UpdateAsync(_mapper.Map<EmployeeOptions>(employeeOptions));
        }

        public async Task<bool> AddRangeAsync(List<CreateEmployeeOptionsDto> createEmployeeOptionsDtos)
        {
            return await _employeeOptionRepository.AddRangeAsync(_mapper.Map<List<EmployeeOptions>>(createEmployeeOptionsDtos));
        }

        public async Task<bool> DeleteEmployeeOptionsAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No EmployeeOptions exists with the provided identifier.");
            }

            return await _employeeOptionRepository.DeleteAsync(id);
        }

        public async Task<bool> EmployeeSurveyStatus(Guid userId, Guid surveyId)
        {
            if(userId == null && surveyId == null)
            {
                throw new NotFoundException("No record exists with the provided identifier.");
            }

            return await _employeeOptionRepository.EmployeeSurveyStatusAsync(userId,surveyId);
        }

        public async Task<IEnumerable<EmployeeOptionsDto>> GetAllEmployeeOptionsAsync()
        {
            var employeeOptions = await _employeeOptionRepository.GetAllAsync();
            if (employeeOptions == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<IEnumerable<EmployeeOptionsDto>>(employeeOptions);
        }

        public async Task<EmployeeOptionsDto?> GetEmployeeOptionsByIdAsync(Guid id)
        {
            var employeeOptions = await _employeeOptionRepository.GetByIdAsync(id);
            if (employeeOptions == null)
            {
                throw new NotFoundException("No EmployeeOptions exists with the provided identifier.");
            }

            return _mapper.Map<EmployeeOptionsDto>(employeeOptions);
        }
    }
}
