using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class EmployeeOptionsController : ControllerBase
    {
        private readonly IEmployeeOptionsService _employeeOptionsService;

        public EmployeeOptionsController(IEmployeeOptionsService employeOptionsService)
        {
            _employeeOptionsService = employeOptionsService;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdateEmployeeOptions(CreateEmployeeOptionsDto employeeOptionsDto)
        {
            var result = await _employeeOptionsService.AddOrUpdateEmployeeOptionsAsync(employeeOptionsDto);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddRange(List<CreateEmployeeOptionsDto> createEmployeeOptionsDtos)
        {
            var result = await _employeeOptionsService.AddRangeAsync(createEmployeeOptionsDtos);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeOptions(Guid id)
        {
            var result = await _employeeOptionsService.DeleteEmployeeOptionsAsync(id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmployeeOptions()
        {
            var employeeOptions = await _employeeOptionsService.GetAllEmployeeOptionsAsync();

            return Ok(employeeOptions);
        }
        
        [HttpGet("{userId}/{surveyId}")]
        public async Task<IActionResult> EmployeeSurveyStatus(Guid userId ,Guid surveyId)
        {
            var status = await _employeeOptionsService.EmployeeSurveyStatus(userId,surveyId);

            return Ok(status);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeOptionsById(Guid id)
        {
            var employeeOptions = await _employeeOptionsService.GetEmployeeOptionsByIdAsync(id);

            return Ok(employeeOptions);
        }

    }
}
