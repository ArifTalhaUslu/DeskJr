using DeskJr.Common.Exceptions;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateSetting(AddOrUpdateSettingDto settingDto)
        {
            var result = await _settingService.AddOrUpdateSettingAsync(settingDto);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSetting(DeleteSettingDto deleteSettingDto)
        {
            var result = await _settingService.DeleteSettingAsync(deleteSettingDto.ID);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSetting()
        {
            var setting = await _settingService.GetAllSettingAsync();

            return Ok(setting);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSettingById(Guid id)
        {
            var setting = await _settingService.GetSettingByIdAsync(id);

            return Ok(setting);
        }

        [HttpPost("update-multiple")]
        public async Task<ActionResult> UpdateMultiple(List<AddOrUpdateSettingDto> settingsDto)
        {
            foreach (var settingDto in settingsDto)
            {
                await _settingService.AddOrUpdateSettingAsync(settingDto);
            }

            return Ok();
        }
    }
}
