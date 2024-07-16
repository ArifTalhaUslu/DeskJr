using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.EmployeeDtos;
using Microsoft.AspNetCore.Http;
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
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
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
            if (employee != null)
            {
                return Ok(employee);
            }
            return BadRequest("Employee not found.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            var result = await _employeeService.UpdateEmployeeAsync(employeeDto);
            if(result)
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
