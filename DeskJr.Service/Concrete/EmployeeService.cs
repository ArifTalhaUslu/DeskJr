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

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUserService userervice)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userService = userervice;
        }

        public async Task<bool> AddOrUpdateEmployeeAsync(AddOrUpdateEmployeeDto employeeDto)
        {
            var currentUserRole = _userService.GetCurrentUserRole();

            if (currentUserRole != EnumRole.Administrator.ToString() && currentUserRole != EnumRole.Manager.ToString())
            {
                throw new UnauthorizedAccessException("You do not have permission to perform this action.");
            }
            if (employeeDto.ID == null && currentUserRole == EnumRole.Administrator.ToString())
            {
                employeeDto.Password = Encrypter.EncryptString(employeeDto.Password);
                return await _employeeRepository.AddAsync(_mapper.Map<Employee>(employeeDto));
            }

            return await _employeeRepository.UpdateAsync(_mapper.Map<Employee>(employeeDto));

        }


        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var currentUserRole = _userService.GetCurrentUserRole();
            if (id == null)
            {
                throw new NotFoundException("No employee exists with the provided identifier.");
            }
            if (currentUserRole != EnumRole.Administrator.ToString())
            {
                throw new UnauthorizedAccessException("You do not have permission to perform this action.");
            }
            return await _employeeRepository.DeleteAsync(id);
        }
            
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var currentUserRole = _userService.GetCurrentUserRole();
            var currentUserIdString = _userService.GetCurrentUserId();
            var currentUserId = new Guid(currentUserIdString);
            IEnumerable<Employee> employees;

            if (currentUserRole != EnumRole.Administrator.ToString() && currentUserRole != EnumRole.Manager.ToString())
            {
                throw new Exception("The requested operation could not be completed. Unauthorized role.");
            }

            if (currentUserRole == EnumRole.Administrator.ToString())
            {
                employees = _employeeRepository.GetListWithIncludeEmployeeAsync();
                return _mapper.Map<IEnumerable<EmployeeDto>>(employees); ;
            }
            if (currentUserRole == EnumRole.Manager.ToString())
            {
                employees = await _employeeRepository.GetTeamEmployeesByIdAsync(currentUserId);
                return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            }
            return Enumerable.Empty<EmployeeDto>();
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

        public async Task<IEnumerable<EmployeeDto>> GetTeamEmployeesByIdAsync(Guid managerId)
        {
            var employees = await _employeeRepository.GetTeamEmployeesByIdAsync(managerId);
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

