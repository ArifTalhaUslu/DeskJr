using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract;

public interface IEmployeeRepository
{
    public Task<bool> AddAsync(Employee employee);
    public Task<bool> UpdateAsync(Employee employee);
    public Task<bool> DeleteAsync(Guid id);
    public Task<List<Employee>> GetAllAsync();
    public Task<Employee> GetByIdAsync(Guid id);

}
