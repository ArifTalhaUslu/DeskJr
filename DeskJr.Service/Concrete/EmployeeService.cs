using AutoMapper;
using DeskJr.Common;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddOrUpdateEmployeeAsync(AddOrUpdateEmployeeDto employeeDto)
        {
            if (employeeDto.ID == null)
            {
                if (string.IsNullOrEmpty(employeeDto.Password)) 
                    throw new BadRequestException("Password is not null field!");

                employeeDto.Password = Encrypter.EncryptString(employeeDto.Password);
                return await _employeeRepository.AddAsync(_mapper.Map<Employee>(employeeDto));
            }
            return await _employeeRepository.UpdateAsync(_mapper.Map<Employee>(employeeDto));
        }


        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = _employeeRepository.GetListWithIncludeEmployeeAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto? GetEmployeeByIdAsync(Guid id)
        {
            var employee = _employeeRepository.GetByIdWithInclude(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByTeamIdAsync(Guid teamId)
        {
            var employees = await _employeeRepository.GetEmployeesByTeamIdAsync(teamId);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _employeeRepository.GetEmployeeByEmailAsync(email);
            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordRequestDTO changePasswordRequest)
        {
            var employee = await _employeeRepository.GetEmployeeByEmailAsync(changePasswordRequest.Email);

            if (employee == null || employee.Password != Encrypter.EncryptString(changePasswordRequest.OldPassword))
            {
                return false;
            }

            if (employee.Password == Encrypter.EncryptString(changePasswordRequest.NewPassword))
            {
                return false;
            }

            employee.Password = Encrypter.EncryptString(changePasswordRequest.NewPassword);
            return await _employeeRepository.UpdateAsync(employee);
        }

    }
}

