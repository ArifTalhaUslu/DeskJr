using System.Text.Json.Serialization;

namespace DeskJr.Entity.Models
{
    public class SurveyQuestion : BaseEntity
    {
        public string Text { get; set; }
        public Guid SurveyId { get; set; }

        [JsonIgnore]
        public Survey Survey { get; set; }
        public IEnumerable<SurveyQuestionOptions> SurveyQuestionOptions { get; set; }

    }
}
