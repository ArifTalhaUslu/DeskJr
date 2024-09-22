using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.SurveyDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdateSurvey(AddOrUpdateSurveyDto surveyDto)
        {
            var result = await _surveyService.AddOrUpdateSurveyAsync(surveyDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSurvey(Guid id)
        {
            var result = await _surveyService.DeleteSurveyAsync(id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSurvey()
        {
            var survey = await _surveyService.GetAllSurveyAsync();

            return Ok(survey);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSurveyById(Guid id)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(id);

            return Ok(survey);
        }

    }
}
