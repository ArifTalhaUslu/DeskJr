using DeskJr.Entity.Models;

namespace DeskJr.Service.Dto.SurveyQuestionDto
{
    public class CreateServeyQuestionDto
    {
        public string Text { get; set; }
        public Guid SurveyId { get; set; }
        public Survey Survey { get; set; }
        public IEnumerable<SurveyQuestionOptions> SurveyQuestionOptions { get; set; }
    }
}
