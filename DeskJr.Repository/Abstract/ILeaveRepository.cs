using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
    public interface ILeaveRepository : IGenericRepository<Leave>
    {
        // Task<List<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId);
      
        Task<List<Leave>> GetLeavesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Leave>> GetLeavesByEmployeeIdAsync(Guid employeeId);
    }
}

