using DeskJr.Service.Abstract;
using DeskJr.Service.Concrete;
using DeskJr.Service.Dto.SurveyQuestionDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class SurveyQuestionController : ControllerBase
    {
        private readonly ISurveyQuestionService _surveyQuestionService;

        public SurveyQuestionController(ISurveyQuestionService surveyQuestionService)
        {
            _surveyQuestionService = surveyQuestionService;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdateSurveyQuestion(AddOrUpdateServeyQuestionDto surveyQuestionDto)
        {
            var result = await _surveyQuestionService.AddOrUpdateSurveyQuestionAsync(surveyQuestionDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyQuestion(Guid id)
        {
            var result = await _surveyQuestionService.DeleteSurveyQuestionAsync(id);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSurveyQuestionByIdAsync(Guid id)
        {
            var result = await _surveyQuestionService.GetSurveyQuestionByIdAsync(id);
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSurveyQuestion()
        {
            var surveyQuestions = await _surveyQuestionService.GetAllSurveyQuestionAsync();

            return Ok(surveyQuestions);
        }

        [HttpGet("GetSurveyQuestionsBySurveyId/{surveyId}")]

        public async Task<ActionResult> GetSurveyQuestionsBySurveyId(Guid surveyId)
        {
            var result = await _surveyQuestionService.GetSurveyQuestionsBySurveyIdAsync(surveyId);

            return Ok(result);
        }


    }
}
