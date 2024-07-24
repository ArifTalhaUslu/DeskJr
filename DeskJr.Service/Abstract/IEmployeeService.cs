using DeskJr.Service.Dto.EmployeeDtos;
using DeskJr.Service.Dto.LeaveDtos;

using DeskJr.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    }
}
