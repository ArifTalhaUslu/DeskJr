using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface IEmployeeService
    {
        public Task<bool> AddOrUpdateEmployeeAsync(AddOrUpdateEmployeeDto employeeDto);
        public Task<bool> DeleteEmployeeAsync(Guid id);
        public Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        public EmployeeDto? GetEmployeeByIdAsync(Guid id);
        public Task<IEnumerable<EmployeeDto>> GetEmployeesByTeamIdAsync(Guid teamId);
        public Task<EmployeeDto?> GetEmployeeByEmailAsync(string email);
        public Task<bool> ChangePasswordAsync(ChangePasswordRequestDTO changePasswordRequest);
        public Task<IEnumerable<EmployeeDto>> GetUpcomingBirthdaysAsync();



    }
}
