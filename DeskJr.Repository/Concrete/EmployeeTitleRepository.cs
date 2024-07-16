using System;
using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
	public class EmployeeTitleRepository :IEmployeeTitleRepository
	{
        private readonly AppDbContext _context;
        public EmployeeTitleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(EmployeeTitle employeeTitle)
        {
            await _context.EmployeeTitles.AddAsync(employeeTitle);
            var affectedRowCount = await _context.SaveChangesAsync();
            return affectedRowCount > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var dbEmployeeTitle = await _context.EmployeeTitles.FirstOrDefaultAsync(e => e.ID == id);
            if (dbEmployeeTitle != null)
            {
                _context.EmployeeTitles.Remove(dbEmployeeTitle);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<EmployeeTitle>> GetAllAsync()
        {
            var dbEmployeeTitles = await _context.EmployeeTitles.ToListAsync();
            return dbEmployeeTitles;
        }

        public async Task<EmployeeTitle?> GetByIdAsync(Guid id)
        {
            var dbEmployeeTitle = await _context.EmployeeTitles.FirstOrDefaultAsync(e => e.ID == id);
            return dbEmployeeTitle;
        }

        public async Task<bool> UpdateAsync(EmployeeTitle request)
        {
            var affectedRowCount = 0;
            var dbEmployeeTitle = await _context.EmployeeTitles.FindAsync(request.ID);
            if (dbEmployeeTitle != null)
            {
                _context.Entry(dbEmployeeTitle).CurrentValues.SetValues(request);

                affectedRowCount = await _context.SaveChangesAsync();
            }
            return affectedRowCount > 0;
        }
    }
}

