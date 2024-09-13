using DeskJr.Service.Abstract;
using DeskJr.Service.Concrete;
using DeskJr.Service.Dto;
using DeskJr.Service.Dto.SurveyDto;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpPost]
        public async Task<ActionResult> AddSurvey(CreateSurveyDto createSurveyDto)
        {
            var result = await _surveyService.AddSurveyAsync(createSurveyDto);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSurvey(Guid id)
        {
            var result = await _surveyService.DeleteSurveyAsync(id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSurvey()
        {
            var employees = await _surveyService.GetAllSurveyAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult GetEmployeeById(Guid id)
        {
            var survey = _surveyService.GetSurveyByIdAsync(id);

            return Ok(survey);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSurvey(SurveyDto surveyDto)
        {
            var result = await _surveyService.UpdateSurveyAsync(surveyDto);

            return Ok(result);
        }

    }
}
