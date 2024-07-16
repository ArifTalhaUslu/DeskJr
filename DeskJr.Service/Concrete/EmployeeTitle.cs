using AutoMapper;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.EmployeeDtos;
using DeskJr.Service.Dto.EmployeeTitleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> AddEmployeeTitleAsync(CreateEmployeeTitleDto employeeTitleDto)
        {
            var employeeTitle = _mapper.Map<EmployeeTitle>(employeeTitleDto);
            return await _employeeTitleRepository.AddAsync(employeeTitle);
        }

        public async Task<bool> DeleteEmployeeTitleAsync(Guid id)
        {
            return await _employeeTitleRepository.DeleteAsync(id);
        }

        public async Task<List<EmployeeTitleDto>> GetAllEmployeeTitlesAsync()
        {
            var employeeTitles = await _employeeTitleRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeTitleDto>>(employeeTitles);
        }

        public async Task<EmployeeTitleDto?> GetEmployeeTitleByIdAsync(Guid id)
        {
            var employeeTitle = await _employeeTitleRepository.GetByIdAsync(id);
            return _mapper.Map<EmployeeTitleDto>(employeeTitle);
        }

        public async Task<bool> UpdateEmployeeTitleAsync(UpdateEmployeeTitleDto employeeTitleDto)
        {
            var employeeTitle = _mapper.Map<EmployeeTitle>(employeeTitleDto);
            return await _employeeTitleRepository.UpdateAsync(employeeTitle);
        }
    }
}


