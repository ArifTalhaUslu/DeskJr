namespace DeskJr.Service.Dto
{
    public class SurveyResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public List<QuestionResultDto> Questions { get; set; } = new();
    }

    public class QuestionResultDto
    {
        public string Text { get; set; } = "";
        public List<OptionResultDto> Options { get; set; } = new();
    }

    public class OptionResultDto 
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = "";
        public int AnswerCount { get; set; }
    }
}
