using DeskJr.Service.Dto;

namespace DeskJr.Services.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<List<LeaveRequestDTO>> GetAllLeaveRequestsAsync();
        Task<LeaveRequestDTO> GetLeaveRequestByIdAsync(Guid id);
        Task<bool> CreateLeaveRequestAsync(LeaveRequestCreateDTO leaveRequestDTO);
        Task<bool> UpdateLeaveRequestAsync(LeaveRequestUpdateDTO leaveRequestDTO);
        Task<bool> DeleteLeaveRequestAsync(Guid id);
        Task<IEnumerable<LeaveRequestDTO>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId);
    }
}
