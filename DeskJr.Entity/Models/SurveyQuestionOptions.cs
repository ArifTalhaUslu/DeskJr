namespace DeskJr.Entity.Models
{
    public class SurveyQuestionOptions : BaseEntity
    {
        public string Text { get; set; }
        public Guid SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
    }
}
