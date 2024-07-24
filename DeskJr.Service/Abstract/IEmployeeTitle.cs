using DeskJr.Entity.Models;
using DeskJr.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Abstract
{
    public interface IEmployeeTitleService
    {
        public Task<bool> AddEmployeeTitleAsync(CreateEmployeeTitleDto employeeTitleDto);
        public Task<bool> UpdateEmployeeTitleAsync(UpdateEmployeeTitleDto employeeTitleDto);
        public Task<bool> DeleteEmployeeTitleAsync(Guid id);
        public Task<List<EmployeeTitleDto>> GetAllEmployeeTitlesAsync();
        public Task<EmployeeTitleDto?> GetEmployeeTitleByIdAsync(Guid id);
    }
}
