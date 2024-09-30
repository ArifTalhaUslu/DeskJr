using DeskJr.Entity.Models;

namespace DeskJr.Service.Dto
{
    public class SurveyQuestionDto
    {
        public Guid Id { get; set; }
        public string text { get; set; }
        public Guid? SurveyId { get; set; }
        public IEnumerable<SurveyQuestionOptions> SurveyQuestionOptions { get; set; }
    }
}
