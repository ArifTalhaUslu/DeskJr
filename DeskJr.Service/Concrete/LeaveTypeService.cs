using System;
using AutoMapper;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Repository.Concrete;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.LeaveDtos;
using DeskJr.Service.Dto.TeamDtos;

namespace DeskJr.Service.Concrete
{
	public class LeaveTypeService: ILeaveTypeService
	{
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddLeaveTypeAsync(LeaveTypeCreateDTO LeaveTypeDto)
        {
            var leaveType = _mapper.Map<LeaveType>(LeaveTypeDto);
            return await _leaveTypeRepository.AddAsync(leaveType);
        }

        public async Task<bool> DeleteLeaveTypeAsync(Guid id)
        {
            return await _leaveTypeRepository.DeleteAsync(id);
        }

        public async Task<List<LeaveTypeDTO>> GetAllLeaveTypesAsync()
        {
            var leaveTypes = await _leaveTypeRepository.GetAllAsync();
            return _mapper.Map<List<LeaveTypeDTO>>(leaveTypes);
        }

        public async Task<LeaveTypeDTO?> GetLeaveTypeByIdAsync(Guid id)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            return _mapper.Map<LeaveTypeDTO>(leaveType);
        }

        public async Task<bool> UpdateLeaveTypeAsync(LeaveTypeUpdateDTO LeaveTypeDto)
        {
            var leaveType = _mapper.Map<LeaveType>(LeaveTypeDto);
            return await _leaveTypeRepository.UpdateAsync(leaveType);
        }
    }
}

