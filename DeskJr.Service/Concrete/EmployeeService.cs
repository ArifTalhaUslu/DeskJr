using AutoMapper;
using DeskJr.Common;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Entity.Types;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using DeskJr.Services.Interfaces;

namespace DeskJr.Service.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILeaveService _leaveService;
        private readonly ISettingService _settingService;


        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUserService userservice, ILeaveService leaveService, ISettingService settingService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userService = userservice;
            _leaveService = leaveService;
            _settingService = settingService;
        }


        public async Task<EmployeeDto?> AuthenticationControlAsync(LoginRequestDTO loginRequest)
        {
            var employee = await _employeeRepository.GetEmployeeByEmailAsync(loginRequest.Email);

            if (employee == null || employee.Password != Encrypter.EncryptString(loginRequest.Password))
            {
                throw new UnauthorizedException("Authentication Failed.");
            }
            if (employee.HireDate > DateTime.Today)
            {
                throw new UnauthorizedException("You are not authorized yet.");
            }

            return _mapper.Map<Employee, EmployeeDto>(employee);
        }


        public async Task<IEnumerable<EmployeeLeavesByDateDto>> CalculateLeaves(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id");
            }

            var currentUser = await _employeeRepository.GetByIdAsync(id);
            var user = _mapper.Map<Employee>(currentUser);

            var employeeLeavesList = new List<EmployeeLeavesByDateDto>();
            var startDate = user.HireDate;
            var endDate = user.HireDate.AddYears(1);

            var allLeaves = await _leaveService.GetLeavesByEmployeeIdAsync();
            float previousTransferDay = 0;
            float deservedDay = 0;

            while (startDate.Year <= DateTime.Now.Year)
            {
                var yearlyLeaves = allLeaves
                    .Where(l => l.StartDate >= startDate && l.EndDate < endDate)
                    .ToList();

                var totalDays = yearlyLeaves.Sum(l => (l.EndDate - l.StartDate).TotalDays);
                var accuredDay = await _settingService.GetAccuredDayAsync();

                var transferDay = (float)(deservedDay - totalDays) + previousTransferDay;

                var employeeLeave = new EmployeeLeavesByDateDto
                {
                    UserId = id,
                    StartDate = startDate,
                    EndDate = endDate,
                    DayUsed = (float)totalDays,
                    DeservedDay = deservedDay,
                    TransferDay = (float)transferDay,
                };

                employeeLeavesList.Add(employeeLeave);

                previousTransferDay = transferDay;
                deservedDay = accuredDay is not null ? Int32.Parse(accuredDay.Value) : 0;
                startDate = endDate;
                endDate = startDate.AddYears(1);
            }

            return employeeLeavesList;
        }
        public async Task<EmployeeLeavesInfoDto> GetEmployeeLeavesInfo(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var employeeLeavesInfo = new EmployeeLeavesInfoDto();
            var currentUser = await _employeeRepository.GetByIdAsync(id);
            var user = _mapper.Map<Employee>(currentUser);

            var startDate = user.HireDate;
            var endDate = user.HireDate.AddYears(1);

            var allLeaves = await _leaveService.GetLeavesByEmployeeIdAsync();
            float deservedDay = 0;
            double totalUsedLeaveDays = 0;
            totalUsedLeaveDays += allLeaves.Sum(l => (l.EndDate - l.StartDate).TotalDays);
            var accuredDay = await _settingService.GetAccuredDayAsync();

            while (startDate.Year <= DateTime.Now.Year)
            {
                employeeLeavesInfo.DeservedDay = deservedDay;
                employeeLeavesInfo.UsedDay = (float)totalUsedLeaveDays;
                employeeLeavesInfo.RemainingDay = deservedDay - (float)totalUsedLeaveDays;

                deservedDay += accuredDay is not null ? Int32.Parse(accuredDay.Value) : 0;

                startDate = endDate;
                endDate = startDate.AddYears(1);
            }

            return employeeLeavesInfo;
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
                return await _employeeRepository.AddAsync(employee);
            }

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