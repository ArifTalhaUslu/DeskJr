using DeskJr.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using DeskJr.Service.Dto;

using DeskJr.Service.Concrete;
using DeskJr.Service.Dto.EmployeeTitleDtos;
using DeskJr.Service.Dto.LeaveDtos;

using Microsoft.AspNetCore.Authorization;


namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeservice;

        public LeaveTypeController(ILeaveTypeService leaveTypeervice)
        {
            _leaveTypeservice = leaveTypeervice;
        }

        [HttpPost]
        public async Task<ActionResult> CreateLeaveType(LeaveTypeUpdateDTO leaveTypeUpdateDto)
        {
            var result = await _leaveTypeservice.AddOrUpdateLeaveTypeAsync(leaveTypeUpdateDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(LeaveTypeDeleteDto leaveTypeDeleteDto)
        {
            var result = await _leaveTypeservice.DeleteLeaveTypeAsync(leaveTypeDeleteDto.Id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllLeaveTypes()
        {
            var leaveTypes = await _leaveTypeservice.GetAllLeaveTypesAsync();
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLeaveTypeById(Guid id)
        {
            var leaveType = await _leaveTypeservice.GetLeaveTypeByIdAsync(id);
            return Ok(leaveType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLeaveType(LeaveTypeUpdateDTO LeaveTypeDto)
        {
            var result = await _leaveTypeservice.UpdateLeaveTypeAsync(LeaveTypeDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
