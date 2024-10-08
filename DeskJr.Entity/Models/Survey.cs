using System.Text.Json.Serialization;

namespace DeskJr.Entity.Models
{
    public class Survey : BaseEntity
    {
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
