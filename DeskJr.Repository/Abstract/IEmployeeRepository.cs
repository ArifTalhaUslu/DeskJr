using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract;

public interface IEmployeeRepository: IGenericRepository<Employee>
{
    
    public Task<IEnumerable<Employee?>> GetEmployeesByManagerIdAsync(Guid managerId);
    public Employee? GetByIdWithInclude(Guid id);
    public Task<Employee?> GetEmployeeByEmailAsync(string email);
    public IEnumerable<Employee> GetListWithInclude();
    public Task<IEnumerable<Employee>> GetUpcomingBirthdaysAsync();
}
