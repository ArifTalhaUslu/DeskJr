﻿using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Dto;
using DeskJr.Services.Interfaces;

namespace DeskJr.Services.Concrete
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LeaveService(ILeaveRepository leaveRepository, IEmployeeRepository employeeRepository, AppDbContext context, IMapper mapper)
        {
            _leaveRepository = leaveRepository;
            _employeeRepository = employeeRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateLeaveAsync(LeaveCreateDTO leaveDTO)
        {

            var leave = _mapper.Map<Leave>(leaveDTO);
            var requestingEmployee = await _employeeRepository.GetByIdAsync(leaveDTO.RequestingEmployeeId);

            if (requestingEmployee == null)
            {
                throw new NotFoundException("Requesting employee not found");
            }
            //requestingEmployee.LeaveRequests.Add(leaveRequest);
            await _employeeRepository.UpdateAsync(requestingEmployee);

            return await _leaveRepository.AddAsync(leave);
        }

        public async Task<bool> DeleteLeaveAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No leave exists with the provided identifier.");
            }

            return await _leaveRepository.DeleteAsync(id);
        }

        public async Task<List<LeaveDTO>> GetAllLeavesAsync()
        {
            var leaves = await _leaveRepository.GetAllAsync();
            if (leaves == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<List<LeaveDTO>>(leaves);
        }

        public async Task<LeaveDTO> GetLeaveByIdAsync(Guid id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
            {
                throw new NotFoundException("No leave exists with the provided identifier.");
            }

            return _mapper.Map<LeaveDTO>(leave);
        }

        public async Task<bool> UpdateLeaveAsync(LeaveUpdateDTO leaveDTO)
        {
            var leave = _mapper.Map<Leave>(leaveDTO);
            if (leave == null)
            {
                throw new NotFoundException("No leave exists with the provided identifier.");
            }

            return await _leaveRepository.UpdateAsync(leave);
        }
        public async Task<IEnumerable<LeaveDTO>> GetLeavesByEmployeeIdAsync(Guid employeeId)
        {
            var leaves = await _leaveRepository.GetLeavesByEmployeeIdAsync(employeeId);
            if (leaves == null)
            {
                throw new NotFoundException("No leaves exists with the provided employee identifier.");
            }

            return _mapper.Map<IEnumerable<LeaveDTO>>(leaves);
        }
    }
}
