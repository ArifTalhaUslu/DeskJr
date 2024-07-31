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
            if (leave == null)
                return NotFound();

            return Ok(leave);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeave(LeaveCreateDTO leaveDTO)
        {
            var result = await _leaveService.CreateLeaveAsync(leaveDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeave(LeaveUpdateDTO leaveDTO)
        {
            var result = await _leaveService.UpdateLeaveAsync(leaveDTO);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeave(Guid id)
        {
            var result = await _leaveService.DeleteLeaveAsync(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<LeaveDTO>>> GetLeavesByEmployeeId(Guid employeeId)
        {
            var leaves = await _leaveService.GetLeavesByEmployeeIdAsync(employeeId);
            if (leaves == null || !leaves.Any())
            {
                return NotFound("jlnvfv");
            }
            return Ok(leaves);
        }
    }
}

