using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract;

public interface IEmployeeTitleRepository
{
    public Task<bool> AddAsync(EmployeeTitle employeeTitle);
    public Task<bool> UpdateAsync(EmployeeTitle employeeTtile);
    public Task<bool> DeleteAsync(Guid id);
    public Task<List<EmployeeTitle>> GetAllAsync();
    public Task<EmployeeTitle?> GetByIdAsync(Guid id);

}