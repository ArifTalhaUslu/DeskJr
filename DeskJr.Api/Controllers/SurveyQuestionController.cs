using DeskJr.Service.Abstract;
using DeskJr.Service.Concrete;
using DeskJr.Service.Dto.SurveyQuestionDto;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    public class SurveyQuestionController : ControllerBase
    {
        private readonly ISurveyQuestionService _surveyQuestionService;

        public SurveyQuestionController(ISurveyQuestionService surveyQuestionService)
        {
            _surveyQuestionService = surveyQuestionService;
        }

        [HttpPost]
        public async Task<ActionResult> AddSurvey(CreateServeyQuestionDto createServeyQuestionDto)
        {
            var result = await _surveyQuestionService.AddSurveyQuestionAsync(createServeyQuestionDto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSurvey(S id)
        {
            var result = await _surveyQuestionService.UpdateSurveyQuestionAsync(id);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSurvey(Guid id)
        {
            var result = await _surveyQuestionService.UpdateSurveyQuestionAsync(id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSurvey()
        {
            var employees = await _surveyQuestionService.GetAllSurveyQuestionAsync();

            return Ok(employees);
        }


    }
}
