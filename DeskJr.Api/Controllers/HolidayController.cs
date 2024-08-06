using DeskJr.Common.Exceptions;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateHoliday(AddOrUpdateHolidayDto holidayDto)
        {
            var result = await _holidayService.AddOrUpdateHolidayAsync(holidayDto);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteHoliday(DeleteHolidayDto deleteHolidayDto)
        {
            var result = await _holidayService.DeleteHolidayAsync(deleteHolidayDto.ID);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllHoliday()
        {
            var holidays = await _holidayService.GetAllHolidaysAsync();
            
            return Ok(holidays);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHolidayById(Guid id)
        {
            var holiday = await _holidayService.GetHolidayByIdAsync(id);
           
            return Ok(holiday);
        }
    }
}
