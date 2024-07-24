using System;
using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
	public interface ILeaveRequestRepository: IGenericRepository<LeaveRequest>
	{
       // Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId);
        Task<List<LeaveRequest>> GetPendingLeaveRequestsAsync();
        Task<List<LeaveRequest>> GetApprovedLeaveRequestsAsync();
        Task<List<LeaveRequest>> GetCancelledLeaveRequestsAsync();
        Task<List<LeaveRequest>> GetLeaveRequestsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId);
    }
}

