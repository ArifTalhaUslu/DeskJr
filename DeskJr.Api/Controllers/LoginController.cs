using Microsoft.AspNetCore.Mvc;
using DeskJr.Service.Abstract;
using System.Threading.Tasks;
using System;
using DeskJr.Service.Dto.LoginDtos;
using DeskJr.Service.Dto.EmployeeDtos;

namespace DeskJr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public LoginController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(loginRequest.Id);

            if (employee != null && employee.Name == loginRequest.Password)
            {
                return Ok(employee);
            }

            return Unauthorized(new { message = "Invalid ID or Password" });
        }
    }
}
