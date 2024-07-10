using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            var affectedRowCount = await _context.SaveChangesAsync();
            return affectedRowCount > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var dbEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.ID == id);
            if (dbEmployee != null)
            {
                _context.Employees.Remove(dbEmployee);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var dbEmployees = await _context.Employees.ToListAsync();
            return dbEmployees;
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            var dbEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.ID == id);
            return dbEmployee;
        }

        public async Task<bool> UpdateAsync(Employee request)
        {
            var affectedRowCount = 0;
            var dbEmployee = await _context.Employees.FindAsync(request.ID);
            if (dbEmployee != null)
            {
                _context.Entry(dbEmployee).CurrentValues.SetValues(request);

                affectedRowCount = await _context.SaveChangesAsync();
            }
            return affectedRowCount > 0;
        }
    }
    
}
