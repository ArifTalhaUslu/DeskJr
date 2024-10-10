using DeskJr.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _loggerService;
        public LogController(ILogService loggerService)
        {
           _loggerService = loggerService;
        }

        [HttpGet]
        public IActionResult GetLogs()
        {
            var logs = _loggerService.GetAllLogsAsync();
            return Ok(logs);
        }


    }
}
