using DeskJr.Entity.Models;
using DeskJr.Service.Dto.EmployeeDtos;
using DeskJr.Service.Dto.LeaveDtos;

namespace DeskJr.Service.Abstract
{
    public interface IEmployeeService
    {
        public Task<bool> AddOrUpdateEmployeeAsync(UpdateEmployeeDto employeeDto);
        public Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto employeeDto);
        public Task<bool> DeleteEmployeeAsync(Guid id);
        public Task<List<EmployeeDto>> GetAllEmployeesAsync();
        public Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);
        public Task<IEnumerable<EmployeeDto>> GetEmployeesByTeamIdAsync(Guid teamId);
       
        

        }
}
