using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface IEmployeeService
    {
        public Task<bool> AddEmployeeAsync(CreateEmployeeDto employeeDto);
        public Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto employeeDto);
        public Task<bool> DeleteEmployeeAsync(Guid id);
        public Task<List<EmployeeDto>> GetAllEmployeesAsync();
        public Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);
        public Task<IEnumerable<EmployeeDto>> GetEmployeesByTeamIdAsync(Guid teamId);
        public Task<EmployeeDto?> GetEmployeeByEmailAsync(string email);



    }
}
