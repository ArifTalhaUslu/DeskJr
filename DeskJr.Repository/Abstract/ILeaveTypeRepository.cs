using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<bool> HasRelatedLeavesAsync(Guid id);
    }
}

