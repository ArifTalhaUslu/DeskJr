using AutoMapper;
using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Dto;
using DeskJr.Services.Interfaces;

namespace DeskJr.Services.Concrete
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, AppDbContext context, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _employeeRepository = employeeRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateLeaveRequestAsync(LeaveRequestCreateDTO leaveRequestDTO)
        {

            var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDTO);
            var requestingEmployee = await _employeeRepository.GetByIdAsync(leaveRequestDTO.RequestingEmployeeId);

            if (requestingEmployee == null)
            {
                throw new Exception("Requesting employee not found");
            }
            //requestingEmployee.LeaveRequests.Add(leaveRequest);
            await _employeeRepository.UpdateAsync(requestingEmployee);

            return await _leaveRequestRepository.AddAsync(leaveRequest);
        }

        public async Task<bool> DeleteLeaveRequestAsync(Guid id)
        {
            return await _leaveRequestRepository.DeleteAsync(id);
        }

        public async Task<List<LeaveRequestDTO>> GetAllLeaveRequestsAsync()
        {
            var leaveRequest = await _leaveRequestRepository.GetAllAsync();
            return _mapper.Map<List<LeaveRequestDTO>>(leaveRequest);
        }

        public async Task<LeaveRequestDTO> GetLeaveRequestByIdAsync(Guid id)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
            return _mapper.Map<LeaveRequestDTO>(leaveRequest);
        }

        public async Task<bool> UpdateLeaveRequestAsync(LeaveRequestUpdateDTO leaveRequestDTO)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDTO);
            return await _leaveRequestRepository.UpdateAsync(leaveRequest);
        }
        public async Task<IEnumerable<LeaveRequestDTO>> GetLeaveRequestsByEmployeeIdAsync(Guid employeeId)
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsByEmployeeIdAsync(employeeId);
            return _mapper.Map<IEnumerable<LeaveRequestDTO>>(leaveRequests);
        }
    }
}
