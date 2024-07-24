using System;
using DeskJr.Service.Dto.LeaveDtos;
using DeskJr.Service.Dto.TeamDtos;

namespace DeskJr.Service.Abstract
{
	public interface ILeaveTypeService
	{
        public Task<bool> AddLeaveTypeAsync(LeaveTypeCreateDTO LeaveTypeDto);
        public Task<bool> UpdateLeaveTypeAsync(LeaveTypeUpdateDTO LeaveTypeDto);
        public Task<bool> DeleteLeaveTypeAsync(Guid id);
        public Task<List<LeaveTypeDTO>> GetAllLeaveTypesAsync();
        public Task<LeaveTypeDTO?> GetLeaveTypeByIdAsync(Guid id);
    }
}

