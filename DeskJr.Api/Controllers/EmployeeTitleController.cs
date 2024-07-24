using DeskJr.Service.Abstract;

using DeskJr.Service.Dto.EmployeeTitleDtos;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTitleController : ControllerBase
    {
        private readonly IEmployeeTitleService _employeeTitleService;

        public EmployeeTitleController(IEmployeeTitleService employeTitleService)
        {
            _employeeTitleService = employeTitleService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployeeTitle(CreateEmployeeTitleDto employeeTitleDto)
        {
            var result = await _employeeTitleService.AddEmployeeTitleAsync(employeeTitleDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeTitle(Guid id)
        {
            var result = await _employeeTitleService.DeleteEmployeeTitleAsync(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
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
