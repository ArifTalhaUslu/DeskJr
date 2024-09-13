using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;


namespace DeskJr.Service.Concrete
{
    public class EmployeeTitleService : IEmployeeTitleService
    {
        private readonly IEmployeeTitleRepository _employeeTitleRepository;
        private readonly IMapper _mapper;

        public EmployeeTitleService(IEmployeeTitleRepository employeeTitleRepository, IMapper mapper)
        {
            _employeeTitleRepository = employeeTitleRepository;
            _mapper = mapper;
        }


        public async Task<bool> AddOrUpdateEmployeeAsync(UpdateEmployeeTitleDto employeeTitleDto)
        {

            if (employeeTitleDto.ID == null)
            {
                return await _employeeTitleRepository.AddAsync(_mapper.Map<EmployeeTitle>(employeeTitleDto));
            }                
            
            return await _employeeTitleRepository.UpdateAsync(_mapper.Map<EmployeeTitle>(employeeTitleDto));  
        }

        public async Task<bool> DeleteEmployeeTitleAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No title exists with the provided identifier.");
            }

            return await _employeeTitleRepository.DeleteAsync(id);
        }

        public async Task<List<EmployeeTitleDto>> GetAllEmployeeTitlesAsync()
        {
            var employeeTitles = await _employeeTitleRepository.GetAllAsync();
            if (employeeTitles == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<List<EmployeeTitleDto>>(employeeTitles);
        }

        public async Task<EmployeeTitleDto?> GetEmployeeTitleByIdAsync(Guid id)
        {
            var employeeTitle = await _employeeTitleRepository.GetByIdAsync(id);
            if (employeeTitle == null)
            {
                throw new NotFoundException("No title exists with the provided identifier.");
            }

            return _mapper.Map<EmployeeTitleDto>(employeeTitle);
        }

        public async Task<bool> UpdateEmployeeTitleAsync(UpdateEmployeeTitleDto employeeTitleDto)
        {
            var employeeTitle = _mapper.Map<EmployeeTitle>(employeeTitleDto);
            if (employeeTitle == null)
            {
                throw new NotFoundException("No title exists with the provided identifier.");
            }

            return await _employeeTitleRepository.UpdateAsync(employeeTitle);
        }
    }
}


