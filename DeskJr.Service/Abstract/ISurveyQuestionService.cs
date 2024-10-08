
using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface ISurveyQuestionService
    {
        public Task<bool> AddOrUpdateSurveyQuestionAsync(AddOrUpdateServeyQuestionDto surveyQuestionDto);
        public Task<bool> DeleteSurveyQuestionAsync(Guid id);
        public Task<List<SurveyQuestionDto>> GetAllSurveyQuestionAsync();
        public Task<SurveyQuestionDto> GetSurveyQuestionByIdAsync(Guid id);
        public Task<List<SurveyQuestionDto>> GetSurveyQuestionsBySurveyIdAsync(Guid surveyId);
    }
}
