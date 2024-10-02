using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SurveyQuestionOptionsController : ControllerBase
    {
        private readonly ISurveyQuestionsOptionsService _surveyQuestionsOptionsService;

        public SurveyQuestionOptionsController(ISurveyQuestionsOptionsService surveyQuestionsOptionsService)
        {
            _surveyQuestionsOptionsService = surveyQuestionsOptionsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSurveyQuestionOptions(AddOrUpdateSurveyQuestionOptionsDto createServeyQuestionOptionsDto)
        {
            var result = await _surveyQuestionsOptionsService.AddSurveyQuestionOptionsAsync(createServeyQuestionOptionsDto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSurveyQuestionOptions(AddOrUpdateSurveyQuestionOptionsDto surveyQuestionOptionsDto)
        {
            var result = await _surveyQuestionsOptionsService.UpdateSurveyQuestionOptionsAsync(surveyQuestionOptionsDto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSurveyQuestionOptions(Guid id)
        {
            var result = await _surveyQuestionsOptionsService.DeleteSurveyQuestionOptionsAsync(id);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSurveyQuestionOptions()
        {
            var surveyQuestionOptions = await _surveyQuestionsOptionsService.GetAllSurveyQuestionOptionsAsync();

            return Ok(surveyQuestionOptions);
        }

        [HttpGet("GetSurveyQuestionsOptionsById/{id}")]
        public async Task<ActionResult> GetSurveyQuestionsOptionsById(Guid id)
        {
            var result = await _surveyQuestionsOptionsService.GetSurveyQuestionOptionsByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("GetSurveyQuestionOptionsBySurveyQuestion/{surveyQuestionId}")]
        public async Task<ActionResult> GetSurveyQuestionOptionsBySurveyQuestiton(Guid surveyQuestionId)
        {
            var result = await _surveyQuestionsOptionsService.GetSurveyQuestionOptionsBySurveyQuestionAsync(surveyQuestionId);

            return Ok(result);
        }

    }
}
