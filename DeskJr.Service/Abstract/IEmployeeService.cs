using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface IEmployeeService
    {
        public Task<bool> AddOrUpdateEmployeeAsync(AddOrUpdateEmployeeDto employeeDto);
        public Task<bool> DeleteEmployeeAsync(Guid id);
        public Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        public Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);
        public Task<IEnumerable<EmployeeDto>> GetEmployeesByTeamIdAsync(Guid teamId);
        public Task<EmployeeDto?> GetEmployeeByEmailAsync(string email);



    }
}
