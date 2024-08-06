using DeskJr.Common.Exceptions;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeService)
        {
            _employeeService = employeService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateEmployee(AddOrUpdateEmployeeDto employeeDto)
        {
            var result = await _employeeService.AddOrUpdateEmployeeAsync(employeeDto);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(DeleteEmployeeDto deleteEmployeeDto)
        {
            var result = await _employeeService.DeleteEmployeeAsync(deleteEmployeeDto.Id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployee()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult GetEmployeeById(Guid id)
        {
            var employee = _employeeService.GetEmployeeByIdAsync(id);
            
            return Ok(employee);
        }

        [HttpGet("teams/{teamId}/employees")]
        public async Task<IActionResult> GetEmployeesByTeamId(Guid teamId)
        {
            var employees = await _employeeService.GetEmployeesByTeamIdAsync(teamId);
            
            return Ok(employees);
        }

        //employee Managerları çekecek endpoint gerekli
    }
}
