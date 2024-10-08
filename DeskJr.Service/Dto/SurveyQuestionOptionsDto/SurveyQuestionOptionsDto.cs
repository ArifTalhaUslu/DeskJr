
using DeskJr.Entity.Models;

namespace DeskJr.Service.Dto
{
    public class SurveyQuestionOptionsDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid SurveyQuestionId { get; set; }
    }
}
