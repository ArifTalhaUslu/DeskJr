namespace DeskJr.Service.Dto.SurveyQuestionDto
{
    public class AddOrUpdateServeyQuestionDto
    {
        public Guid? Id { get; set; }
        public string Text { get; set; }
        public Guid? SurveyId { get; set; }
    }
}
