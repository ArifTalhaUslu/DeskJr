using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee?>> GetTeamEmployeesByIdAsync(Guid managerId)
        {
           return await _context.Employees
                .Include(e => e.EmployeeTitle)
                .Include(e => e.Team)
                .Where(e => e.Team != null && e.Team.ManagerId == managerId)
                .ToListAsync();
        }
        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees
                .Include(e => e.EmployeeTitle)
                .Include(e => e.Team)
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public IEnumerable<Employee> GetListWithIncludeEmployeeAsync()
        {
            return _context.Employees
                .Include(x => x.EmployeeTitle)
                .Include(x => x.Team)
                .ToList();
        }

        public Employee? GetByIdWithInclude(Guid id)
        {
            return _context.Employees.Include(x => x.Team).Include(x => x.EmployeeTitle).FirstOrDefault(x => x.ID == id);
        }
        public async Task<IEnumerable<Employee>> GetUpcomingBirthdaysAsync()
        {
            var now = DateTime.Now;
            var oneMonthLater = now.AddMonths(1);

            var employees = await _context.Employees
                .Where(x =>
                    (x.DayOfBirth.Month == now.Month && x.DayOfBirth.Day >= now.Day) ||
                    (x.DayOfBirth.Month == oneMonthLater.Month && x.DayOfBirth.Day <= oneMonthLater.Day) ||
                    (x.DayOfBirth.Month > now.Month && x.DayOfBirth.Month < oneMonthLater.Month))
                .ToListAsync();

            return employees;
        }
    }
}
