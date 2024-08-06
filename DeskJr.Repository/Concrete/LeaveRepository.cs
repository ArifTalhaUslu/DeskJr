using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class LeaveRepository : GenericRepository<Leave>, ILeaveRepository

    {
        private readonly AppDbContext _context;

        public LeaveRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

      

        public async Task<List<Leave>> GetLeavesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Leaves
               .Where(lr => lr.StartDate >= startDate && lr.EndDate <= endDate)
               .ToListAsync();
        }

        //public async Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId)
        //{
        //return await _context.LeaveRequests
        //     .Where(lr => lr.RequestingEmployeeId == employeeId)
        //       .ToListAsync();
        //}

      
        public async Task<IEnumerable<Leave>> GetLeavesByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.Leaves
                                 .Where(lr => lr.RequestingEmployeeId == employeeId)
                                 .Include(e => e.LeaveType)
                                 .ToListAsync();
        }
    }
}

