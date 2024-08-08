using DeskJr.Entity.Types;
using DeskJr.Service.Dto;

namespace DeskJr.Services.Interfaces
{
    public interface ILeaveService
    {
        Task<List<LeaveDTO>> GetAllLeavesAsync();
        Task<LeaveDTO> GetLeaveByIdAsync(Guid id);
        Task<bool> CreateLeaveAsync(LeaveCreateDTO leaveDTO);
        Task<bool> UpdateLeaveAsync(LeaveUpdateDTO leaveDTO);
        Task<bool> DeleteLeaveAsync(Guid id);
        Task<IEnumerable<LeaveDTO>> GetLeavesByEmployeeIdAsync(Guid employeeId);
        Task<IEnumerable<LeaveDTO>> GetPendingLeavesForApproverEmployeeByEmployeeId(Guid currentUserId);
        Task<bool> UpdateLeaveStatus(UpdateLeaveStatusDto request);
    }
}
