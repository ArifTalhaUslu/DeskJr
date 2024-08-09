using DeskJr.Entity.Types;
using DeskJr.Service.Dto;
using DeskJr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeaves()
        {
            var leaves = await _leaveService.GetAllLeavesAsync();
            return Ok(leaves);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveById(Guid id)
        {
            var leave = await _leaveService.GetLeaveByIdAsync(id);

            return Ok(leave);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeave(LeaveCreateDTO leaveDTO)
        {
            var result = await _leaveService.CreateLeaveAsync(leaveDTO);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeave(LeaveUpdateDTO leaveDTO)
        {
            var result = await _leaveService.UpdateLeaveAsync(leaveDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeave(Guid id)
        {
            var result = await _leaveService.DeleteLeaveAsync(id);

            return Ok();
        }

        [HttpGet("leaveByEmployeeId/{employeeId}")]
        public async Task<ActionResult<IEnumerable<LeaveDTO>>> GetLeavesByEmployeeId(Guid employeeId)
        {
            var leaves = await _leaveService.GetLeavesByEmployeeIdAsync(employeeId);
            return Ok(leaves);
        }

        [HttpPost("pendingLeaves")]
        public async Task<ActionResult<IEnumerable<LeaveDTO>>> GetPendingLeavesForApproverEmployeeByEmployeeId(PendingLeaveRequestDto request)
        {
            var leaves = await _leaveService.GetPendingLeavesForApproverEmployeeByEmployeeId(request.currentUserId, (int)request.role);
            return Ok(leaves);
        }

        [HttpPost("updateStatus")]
        public async Task<ActionResult<IEnumerable<LeaveDTO>>> UpdateLeaveStatus(UpdateLeaveStatusDto request)
        {
            var leaves = await _leaveService.UpdateLeaveStatus(request);
            return Ok(leaves);
        }

        [HttpGet("recentValidLeaves")]
        public async Task<IActionResult> GetRecentValidLeaves()
        {
            var validLeaves = await _leaveService.GetValidLeaves();
            return Ok(validLeaves);
        }
    }
}

