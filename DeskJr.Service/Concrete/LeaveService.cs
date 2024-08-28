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
        private readonly IUserService _userService;

        public LeaveService(ILeaveRepository leaveRepository, IEmployeeRepository employeeRepository, AppDbContext context, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ITeamRepository teamRepository, EmailSender emailSender, IUserService userservice)
        {
            _leaveRepository = leaveRepository;
            _employeeRepository = employeeRepository;
            _context = context;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _teamRepository = teamRepository;
            _sender = emailSender;
            _userService = userservice;
        }

        public async Task<bool> CreateLeaveAsync(LeaveCreateDTO leaveDTO)
        {
            var requestingEmployeeId = _userService.GetCurrentUser().UserId;
            var leave = _mapper.Map<Leave>(leaveDTO);
            var requestingEmployee = _employeeRepository.GetByIdWithInclude(requestingEmployeeId);

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

            var requestingEmployeeTeam = requestingEmployee.Team;
            if (requestingEmployeeTeam is not null && requestingEmployeeTeam.ManagerId is not null)
            {
                var manager = await _employeeRepository.GetByIdAsync(requestingEmployeeTeam.ManagerId.Value);
                if (manager is not null)
                {
                    await SendLeaveRequestNotificationAsync(manager.Email, manager.Name, requestingEmployee.Name, leave.StartDate, leave.EndDate, leave.RequestComments);
                }
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
        public async Task<IEnumerable<LeaveDTO>> GetLeavesByEmployeeIdAsync()
        {
            var employeeId = _userService.GetCurrentUser().UserId;
            var leaves = await _leaveRepository.GetLeavesByEmployeeIdAsync(employeeId);
            if (leaves == null)
            {
                throw new NotFoundException("No leaves exists with the provided employee identifier.");
            }

            return _mapper.Map<IEnumerable<LeaveDTO>>(leaves);
        }
        public async Task<IEnumerable<LeaveDTO>> GetPendingLeavesForApproverEmployeeByEmployeeId()
        {
            var currentUser = _userService.GetCurrentUser();
            var leaves = await _leaveRepository.GetPendingLeavesForApproverEmployeeByEmployeeId(currentUser.UserId, (int)currentUser.Role);
            if (leaves == null)
            {
                throw new NotFoundException("No leaves exists with the provided employee identifier.");
            }

            return _mapper.Map<IEnumerable<LeaveDTO>>(leaves);
        }

        public async Task<bool> UpdateLeaveStatus(UpdateLeaveStatusDto request)
        {
            var currentUser = _userService.GetCurrentUser();
            var leaveToBeUpdated = await _context.Leaves.FirstOrDefaultAsync(x => x.ID == request.LeaveId);
            if (leaveToBeUpdated == null)
                throw new NotFoundException("No leave exists");

            leaveToBeUpdated.StatusOfLeave = request.NewStatus;
            leaveToBeUpdated.ApprovedById = currentUser.UserId;
            leaveToBeUpdated.ConfirmDescription = request.ConfirmDescription;

            if (leaveToBeUpdated.ApprovedById.HasValue)
            {
                leaveToBeUpdated.ApprovedBy = await _context.Employees.FirstOrDefaultAsync(e => e.ID == currentUser.UserId);
            }

            var result = await _leaveRepository.UpdateAsync(leaveToBeUpdated);

            var requestingEmployee = _employeeRepository.GetByIdWithInclude(leaveToBeUpdated.RequestingEmployeeId);
            var requestingEmployeeTeam = requestingEmployee?.Team;
            var manager = await _employeeRepository.GetByIdAsync(requestingEmployeeTeam is not null ? requestingEmployeeTeam.ManagerId.HasValue ? requestingEmployeeTeam.ManagerId.Value : Guid.Empty : Guid.Empty);
            var isApproved = (request.NewStatus == EnumStatusOfLeave.Approved);

            if (manager is not null && requestingEmployee is not null)
            {
                await SendLeaveRequestResponseAsync(requestingEmployee.Email, requestingEmployee.Name, leaveToBeUpdated.StartDate, leaveToBeUpdated.EndDate, isApproved, leaveToBeUpdated.ConfirmDescription);
            }

            return result;
        }
        public async Task<List<LeaveDTO>> GetValidLeaves()
        {
            var validLeaves = await _leaveRepository.GetValidLeaves();
            return _mapper.Map<List<LeaveDTO>>(validLeaves);
        }


        private async Task SendLeaveRequestNotificationAsync(string toEmail, string teamLeaderName, string employeeName, DateTime startDate, DateTime endDate, string requestComments)
        {
            string template = EmailTemplates.LeaveRequestNotificationTemplate;
            var variables = new Dictionary<string, string>
            {
                { "TeamLeaderName", teamLeaderName },
                { "EmployeeName", employeeName },
                { "StartDate", startDate.ToShortDateString() },
                { "EndDate", endDate.ToShortDateString() },
                { "RequestComments", requestComments },
            };
            await _sender.SendEmailAsync(toEmail, "İzin Talebi Bildirimi", template, variables);
        }

        public async Task SendLeaveRequestResponseAsync(string toEmail, string employeeName, DateTime startDate, DateTime endDate, bool isApproved, string? confirmDescription)
        {
            string template = EmailTemplates.LeaveRequestResponseTemplate;
            var variables = new Dictionary<string, string>
            {
                { "EmployeeName", employeeName },
                { "StartDate", startDate.ToShortDateString() },
                { "EndDate", endDate.ToShortDateString() },
                { "ApprovalStatus", isApproved ? "Onaylanmıştır" : "Reddedilmiştir" },
                { "ApprovalStatusClass", isApproved ? "approved" : "reject" },
                { "ConfirmDescription", isApproved ? "": $"<strong>{confirmDescription}</strong> açıklaması ile" }
            };
            await _sender.SendEmailAsync(toEmail, $"İzin Talebi {variables["ApprovalStatus"]}", template, variables);
        }
        public async Task<IEnumerable<LeaveDTO>> GetLeavesByManagerId()
        {
            var currentUserId = _userService.GetCurrentUser().UserId;
            var leaves = await _leaveRepository.GetLeavesWithIncludeByManagerId(currentUserId);
            if (leaves == null)
            {
                throw new NotFoundException("No leaves exists with the provided employee identifier.");
            }

            return _mapper.Map<IEnumerable<LeaveDTO>>(leaves);
        }
    }
}
