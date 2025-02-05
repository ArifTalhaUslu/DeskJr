using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface IEmployeeService
    {
        public Task<bool> AddOrUpdateEmployeeAsync(AddOrUpdateEmployeeDto employeeDto);
        public Task<bool> DeleteEmployeeAsync(Guid id);
        public Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        public EmployeeDto? GetEmployeeByIdAsync(Guid id);
        public Task<IEnumerable<EmployeeDto>> GetEmployeesByManagerIdAsync(Guid managerId);
        public Task<EmployeeDto?> GetEmployeeByEmailAsync(string email);
        public Task<bool> ChangePasswordAsync(ChangePasswordRequestDTO changePasswordRequest);
        public Task<IEnumerable<EmployeeDto>> GetUpcomingBirthdaysAsync();
        public Task<IEnumerable<EmployeeLeavesByDateDto>> CalculateLeaves(Guid id);
        public Task<EmployeeLeavesInfoDto> GetEmployeeLeavesInfo(Guid id);
        public Task<EmployeeDto?> AuthenticationControlAsync(LoginRequestDTO loginRequest);
        
    }   
}
