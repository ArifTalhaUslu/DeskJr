using AutoMapper;
using DeskJr.Common;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Entity.Types;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUserService userservice)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userService = userservice;
        }

        public async Task<bool> AddOrUpdateEmployeeAsync(AddOrUpdateEmployeeDto employeeDto)
        {
            var currentUser = _userService.GetCurrentUser();

            if (currentUser.Role != EnumRole.Administrator && currentUser.Role != EnumRole.Manager)
            {
                throw new UnauthorizedAccessException("You do not have permission to perform this action.");
            }
          
            var employee = _mapper.Map<Employee>(employeeDto);

            if (employeeDto.ID == null && currentUser.Role == EnumRole.Administrator)
            {
                employee.Password = Encrypter.EncryptString(employeeDto.Password);
                employee.Base64Image = employeeDto.Base64Image;
                return await _employeeRepository.AddAsync(employee);
            }

            employee.Base64Image = employeeDto.Base64Image;

            return await _employeeRepository.UpdateAsync(employee);
        }


        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var currentUser = _userService.GetCurrentUser();

            if (id == Guid.Empty)
            {
                throw new NotFoundException("No employee exists with the provided identifier.");
            }

            if (currentUser.Role != EnumRole.Administrator)
            {
                throw new UnauthorizedAccessException("You do not have permission to perform this action.");
            }

            return await _employeeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var currentUser = _userService.GetCurrentUser();
            IEnumerable<Employee> employees;

            if (currentUser.Role != EnumRole.Administrator && currentUser.Role != EnumRole.Manager)
            {
                throw new UnauthorizedAccessException("The requested operation could not be completed. Unauthorized role.");
            }

            if (currentUser.Role == EnumRole.Administrator)
            {
                employees = _employeeRepository.GetListWithInclude();
            }
            else if (currentUser.Role == EnumRole.Manager)
            {
                employees = await _employeeRepository.GetEmployeesByManagerIdAsync(currentUser.UserId);
            }
            else
            {
                return Enumerable.Empty<EmployeeDto>();
            }

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto? GetEmployeeByIdAsync(Guid id)
        {
            var employee = _employeeRepository.GetByIdWithInclude(id);
            if (employee == null)
            {
                throw new NotFoundException("No employee exists with the provided identifier.");
            }

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByManagerIdAsync(Guid managerId)
        {
            var employees = await _employeeRepository.GetEmployeesByManagerIdAsync(managerId);
            if (employees == null)
            {
                throw new NotFoundException("No employee exists with the provided team identifier.");
            }

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _employeeRepository.GetEmployeeByEmailAsync(email);
            if (string.IsNullOrEmpty(email))
            {
                throw new NotFoundException("No employee exists with the provided email.");
            }

            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordRequestDTO changePasswordRequest)
        {
            var employee = await _employeeRepository.GetByIdAsync(changePasswordRequest.ID);

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
        public async Task<IEnumerable<EmployeeDto>> GetUpcomingBirthdaysAsync()
        {
            var employees = await _employeeRepository.GetUpcomingBirthdaysAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }
    }
}

