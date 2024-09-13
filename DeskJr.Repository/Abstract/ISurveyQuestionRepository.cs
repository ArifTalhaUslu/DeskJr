using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
    public interface ISurveyQuestionRepository : IGenericRepository<SurveyQuestion>
    {
        public Task<List<SurveyQuestion>> GetSurveyQuestionsBySurveyIdAsync(Guid surveyId);

    }
}
