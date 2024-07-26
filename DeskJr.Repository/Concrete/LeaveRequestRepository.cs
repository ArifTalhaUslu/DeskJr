using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository

    {
        private readonly AppDbContext _context;

        public LeaveRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LeaveRequest>> GetApprovedLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                .Where(lr => lr.Approved == true)
                .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetCancelledLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                 .Where(lr => lr.Cancelled)
                 .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.LeaveRequests
               .Where(lr => lr.StartDate >= startDate && lr.EndDate <= endDate)
               .ToListAsync();
        }

        //public async Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId)
        //{
        //return await _context.LeaveRequests
        //     .Where(lr => lr.RequestingEmployeeId == employeeId)
        //       .ToListAsync();
        //}

        public async Task<List<LeaveRequest>> GetPendingLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                 .Where(lr => lr.Approved == null)
                 .ToListAsync();
        }
        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.LeaveRequests
                                 .Where(lr => lr.RequestingEmployeeId == employeeId)
                                 .ToListAsync();
        }
    }
}

