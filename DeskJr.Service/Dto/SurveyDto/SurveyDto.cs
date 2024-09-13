using DeskJr.Entity.Models;

namespace DeskJr.Service.Dto.SurveyDto
{
    public class SurveyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
