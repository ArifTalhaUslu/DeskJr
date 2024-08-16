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
        Task<IEnumerable<LeaveDTO>> GetLeavesByEmployeeIdAsync();
        Task<IEnumerable<LeaveDTO>> GetPendingLeavesForApproverEmployeeByEmployeeId();
        Task<bool> UpdateLeaveStatus(UpdateLeaveStatusDto request);
        Task<List<LeaveDTO>> GetValidLeaves();
    }
}
