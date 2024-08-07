using DeskJr.Service.Abstract;
using DeskJr.Service.Concrete;
using DeskJr.Service.Dto;

using DeskJr.Service.Dto.EmployeeTitleDtos;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeTitleController : ControllerBase
    {
        private readonly IEmployeeTitleService _employeeTitleService;

        public EmployeeTitleController(IEmployeeTitleService employeTitleService)
        {
            _employeeTitleService = employeTitleService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployeeTitle(UpdateEmployeeTitleDto employeeTitleDto)
        {
            var result = await _employeeTitleService.AddOrUpdateEmployeeAsync(employeeTitleDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(DeleteEmployeeTitleDto deleteEmployeeTitleDto)
        {
            var result = await _employeeTitleService.DeleteEmployeeTitleAsync(deleteEmployeeTitleDto.Id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployeeTitle()
        {
            var employeeTitles = await _employeeTitleService.GetAllEmployeeTitlesAsync();
            return Ok(employeeTitles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeTitleById(Guid id)
        {
            var employeeTitle = await _employeeTitleService.GetEmployeeTitleByIdAsync(id);
            return Ok(employeeTitle);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployeeTitle(UpdateEmployeeTitleDto employeeTitleDto)
        {
            var result = await _employeeTitleService.UpdateEmployeeTitleAsync(employeeTitleDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
