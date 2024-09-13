
using DeskJr.Entity.Models;

namespace DeskJr.Service.Dto.SurveyQuestionOptionsDto
{
    public class SurveyQuestionOptionsDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
    }
}
