﻿using DeskJr.Service.Dto.EmployeeTitleDtos;

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
