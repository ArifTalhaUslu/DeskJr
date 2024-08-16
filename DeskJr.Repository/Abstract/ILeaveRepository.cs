using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
    public interface ILeaveRepository : IGenericRepository<Leave>
    {
        // Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId);

        Task<List<Leave>> GetLeavesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Leave>> GetLeavesByEmployeeIdAsync(Guid employeeId);
        Task<IEnumerable<Leave>> GetPendingLeavesForApproverEmployeeByEmployeeId(Guid currentUserId, int role);
        Task<IEnumerable<Leave>> GetValidLeaves();
        Task<IEnumerable<Leave>> GetLeavesWithIncludeByManagerId(Guid currentUserId);

    }
}

