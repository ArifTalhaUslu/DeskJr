using DeskJr.Common.Exceptions;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeService)
        {
            _employeeService = employeService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var result = await _employeeService.AddEmployeeAsync(employeeDto);

            if (!result)
            {
                throw new BadRequestException("Employee could not be created.");
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(DeleteEmployeeDto deleteEmployeeDto)
        {
            var result = await _employeeService.DeleteEmployeeAsync(deleteEmployeeDto.Id);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployee()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            var result = await _employeeService.UpdateEmployeeAsync(employeeDto);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
        [HttpGet("teams/{teamId}/employees")]
        public async Task<IActionResult> GetEmployeesByTeamId(Guid teamId)
        {
            var employees = await _employeeService.GetEmployeesByTeamIdAsync(teamId);
            return Ok(employees);
        }
    }
}
