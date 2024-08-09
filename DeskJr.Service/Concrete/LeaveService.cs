using AutoMapper;
using DeskJr.Common;
using DeskJr.Common.Exceptions;
using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Entity.Types;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Concrete;
using DeskJr.Service.Dto;
using DeskJr.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Services.Concrete
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly EmailSender _sender;
        private readonly ITeamRepository _teamRepository;

        public LeaveService(ILeaveRepository leaveRepository, IEmployeeRepository employeeRepository, AppDbContext context, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ITeamRepository teamRepository)
        {
            _leaveRepository = leaveRepository;
            _employeeRepository = employeeRepository;
            _context = context;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _teamRepository = teamRepository;
        }

        public async Task<bool> CreateLeaveAsync(LeaveCreateDTO leaveDTO)
        {
            var leave = _mapper.Map<Leave>(leaveDTO);
            var requestingEmployee = await _employeeRepository.GetByIdAsync(leaveDTO.RequestingEmployeeId);
            var requestingEmployeeTeam = requestingEmployee?.Team;
            var manager = requestingEmployeeTeam?.Manager;

            if (requestingEmployee == null)
            {
                throw new NotFoundException("Requesting employee not found");
            }

            leave.RequestingEmployeeId = requestingEmployee.ID;
            leave.RequestingEmployee = requestingEmployee;
            leave.StatusOfLeave = (int)EnumStatusOfLeave.Pending;

            if (leave.StartDate > leave.EndDate)
            {
                throw new ArgumentException("StartDate cannot be later than the EndDate");
            }

            var result = await _leaveRepository.AddAsync(leave);

            if (manager is not null)
            {
                await SendLeaveRequestNotificationAsync(manager.Email, manager.Name, requestingEmployee.Name, leave.StartDate, leave.EndDate);
            }

            return result;
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

            if (leave.StartDate > leave.EndDate)
            {
                throw new ArgumentException("StartDate cannot be later than the EndDate");
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
        public async Task<IEnumerable<LeaveDTO>> GetPendingLeavesForApproverEmployeeByEmployeeId(Guid currentUserId, int role)
        {
            var leaves = await _leaveRepository.GetPendingLeavesForApproverEmployeeByEmployeeId(currentUserId, role);
            if (leaves == null)
            {
                throw new NotFoundException("No leaves exists with the provided employee identifier.");
            }

            return _mapper.Map<IEnumerable<LeaveDTO>>(leaves);
        }

        public async Task<bool> UpdateLeaveStatus(UpdateLeaveStatusDto request)
        {
            var leaveToBeUpdated = await _context.Leaves.FirstOrDefaultAsync(x => x.ID == request.LeaveId);
            if (leaveToBeUpdated == null)
                throw new NotFoundException("No leave exists");

            leaveToBeUpdated.StatusOfLeave = request.NewStatus;
            leaveToBeUpdated.ApprovedById = request.ApprovedById;

            if (request.ApprovedById.HasValue)
            {
                leaveToBeUpdated.ApprovedBy = await _context.Employees.FirstOrDefaultAsync(e => e.ID == request.ApprovedById);
            }

            return await _leaveRepository.UpdateAsync(leaveToBeUpdated);
        }

        private async Task SendLeaveRequestNotificationAsync(string toEmail, string teamLeaderName, string employeeName, DateTime startDate, DateTime endDate)
        {
            string template = EmailTemplates.LeaveRequestNotificationTemplate;
            var variables = new Dictionary<string, string>
            {
                { "TemaLeaderName", teamLeaderName },
                { "EmployeeName", employeeName },
                { "StartDate", startDate.ToString("yyyy-MM-dd") },
                { "EndDate", endDate.ToString("yyyy-MM-dd") }
            };
            await _sender.SendEmailAsync(toEmail, "İzin Talebi Bildirimi", template, variables);
        }

        public async Task SendLeaveRequestResponseAsync(string toEmail, string employeeName, string startDate, string endDate, bool isApproved)
        {
            string template = EmailTemplates.LeaveRequestResponseTemplate;
            var variables = new Dictionary<string, string>
            {
                { "EmployeeName", employeeName },
                { "StartDate", startDate },
                { "EndDate", endDate },
                { "ApprovalStatus", isApproved ? "Onaylanmıştır" : "Reddedilmiştir" },
                { "ApprovalStatusClass", isApproved ? "" : "reject" }
            };
            await _sender.SendEmailAsync(toEmail, $"İzin Talebi {variables["ApprovalStatus"]}", template, variables);
        }
    }
}
