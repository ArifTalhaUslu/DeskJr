
using DeskJr.Entity.Models;

namespace DeskJr.Service.Dto.SurveyQuestionOptionsDto
{
    public class CreateSurveyQuestionOptionsDto
    {
        public string Text { get; set; }
        public Guid SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
    }
}
