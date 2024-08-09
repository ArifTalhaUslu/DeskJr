using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Repository.Concrete;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddOrUpdateLeaveTypeAsync(LeaveTypeUpdateDTO LeaveTypeUpdateDto)
        {
            if (LeaveTypeUpdateDto.ID == null)
            {
                return await _leaveTypeRepository.AddAsync(_mapper.Map<LeaveType>(LeaveTypeUpdateDto));
            }

            return await _leaveTypeRepository.UpdateAsync(_mapper.Map<LeaveType>(LeaveTypeUpdateDto));
        }

        public async Task<bool> DeleteLeaveTypeAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No Leave type exists with the provided identifier.");
            }

            return await _leaveTypeRepository.DeleteAsync(id);
        }

        public async Task<List<LeaveTypeDTO>> GetAllLeaveTypesAsync()
        {
            var leaveTypes = await _leaveTypeRepository.GetAllAsync();
            if (leaveTypes == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<List<LeaveTypeDTO>>(leaveTypes);
        }

        public async Task<LeaveTypeDTO?> GetLeaveTypeByIdAsync(Guid id)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            if (leaveType== null)
            {
                throw new NotFoundException("No leave type exists with the provided identifier.");
            }

            return _mapper.Map<LeaveTypeDTO>(leaveType);
        }

        public async Task<bool> UpdateLeaveTypeAsync(LeaveTypeUpdateDTO LeaveTypeDto)
        {
            var leaveType = _mapper.Map<LeaveType>(LeaveTypeDto);
            if (leaveType == null)
            {
                throw new NotFoundException("No leave type exists with the provided identifier.");
            }

            return await _leaveTypeRepository.UpdateAsync(leaveType);
        }
    }
}

