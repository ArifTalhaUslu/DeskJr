using DeskJr.Service.Dto.EmployeeDtos;
using DeskJr.Service.Dto.LeaveDtos;
using DeskJr.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            var leaveRequests = await _leaveRequestService.GetAllLeaveRequestsAsync();
            return Ok(leaveRequests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveRequestById(Guid id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
            if (leaveRequest == null)
                return NotFound();

            return Ok(leaveRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest(LeaveRequestCreateDTO leaveRequestDTO)
        {
            var result = await _leaveRequestService.CreateLeaveRequestAsync(leaveRequestDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeaveRequest( LeaveRequestUpdateDTO leaveRequestDTO)
        {
            var result = await _leaveRequestService.UpdateLeaveRequestAsync(leaveRequestDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(Guid id)
        {
             var result = await _leaveRequestService.DeleteLeaveRequestAsync(id);
                if (result)
                {
                    return Ok();
                }
                return NotFound();
        }
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<LeaveRequestDTO>>> GetLeaveRequestsByEmployeeId(Guid employeeId)
        {
            var leaveRequests = await _leaveRequestService.GetLeaveRequestsByEmployeeIdAsync(employeeId);
            if (leaveRequests == null || !leaveRequests.Any())
            {
                return NotFound("jlnvfv");
            }
            return Ok(leaveRequests);
        }
    }
}

