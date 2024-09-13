using DeskJr.Entity.Models;

namespace DeskJr.Service.Dto.SurveyDto
{
    public class CreateSurveyDto
    {
        public string Name { get; set; }
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
