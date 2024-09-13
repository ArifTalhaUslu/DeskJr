using DeskJr.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    public class SurveyQuestionOptionsController : ControllerBase
    {
        private readonly ISurveyQuestionsOptionsService _surveyQuestionsOptionsService;

        public SurveyQuestionOptionsController(ISurveyQuestionsOptionsService surveyQuestionsOptionsService)
        {
            _surveyQuestionsOptionsService = surveyQuestionsOptionsService;
        }


    }
}
