using System.Text.Json.Serialization;

namespace DeskJr.Entity.Models
{
    public class SurveyQuestionOptions : BaseEntity
    {
        public string Text { get; set; }
        public Guid SurveyQuestionId { get; set; }
        
        [JsonIgnore]
        public SurveyQuestion SurveyQuestion { get; set; }
    }
}
