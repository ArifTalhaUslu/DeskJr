using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Entity.Types;
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
               .OrderByDescending(x => x.StartDate).ThenByDescending(x=>x.EndDate)
               .ToListAsync();
        }

        public async Task<IEnumerable<Leave>> GetLeavesByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.Leaves
                                 .Where(x => x.RequestingEmployeeId == employeeId)
                                 .OrderByDescending(x => x.StartDate).ThenByDescending(x => x.EndDate)
                                 .Include(x => x.ApprovedBy)
                                 .Include(x => x.LeaveType)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Leave>> GetPendingLeavesForApproverEmployeeByEmployeeId(Guid currentUserId , int role)
        {
            if (role == (int)EnumRole.Administrator)
            {
                return await _context.Leaves
                    .Include(x => x.RequestingEmployee)
                    .Include(x => x.LeaveType)
                    .Where(x =>
                        x.StatusOfLeave == (int)EnumStatusOfLeave.Pending &&
                        x.RequestingEmployeeId != currentUserId)
                    .OrderByDescending(x => x.StartDate).ThenByDescending(x => x.EndDate)
                    .ToListAsync();
            }
            
            return await _context.Leaves
                .Include(x => x.RequestingEmployee)
                .ThenInclude(x => x.Team)
                .Include(x => x.LeaveType)
                .Where(x => 
                    x.StatusOfLeave == (int)EnumStatusOfLeave.Pending && 
                    x.RequestingEmployeeId != currentUserId && 
                    x.RequestingEmployee.Team != null && 
                    x.RequestingEmployee.Team.ManagerId == currentUserId)
                .OrderByDescending(x => x.StartDate).ThenByDescending(x => x.EndDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Leave>> GetValidLeaves()
        {
            var now = DateTime.Now;
            var oneMonthFromNow = now.AddMonths(1);

            return await _context.Leaves
                .Include(x => x.RequestingEmployee)
                .Where(x =>
                    x.StatusOfLeave == EnumStatusOfLeave.Approved &&
                    x.EndDate > now &&
                    x.StartDate < oneMonthFromNow)
                .OrderBy(x => x.StartDate).ThenBy(x => x.EndDate)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Leave>> GetLeavesWithIncludeByManagerId(Guid currentUserId)
        {
            return await _context.Leaves
                .Include(x => x.RequestingEmployee)
                .ThenInclude(x => x.Team)
                .Include(x => x.LeaveType)
                .Where(x =>
                    x.RequestingEmployeeId != currentUserId &&
                    x.RequestingEmployee.Team != null &&
                    x.RequestingEmployee.Team.ManagerId == currentUserId)
                .OrderByDescending(x => x.StartDate).ThenByDescending(x => x.EndDate)
                .ToListAsync();
        }
    }
}
