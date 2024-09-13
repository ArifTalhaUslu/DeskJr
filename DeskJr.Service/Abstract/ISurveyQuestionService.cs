
using DeskJr.Service.Dto.SurveyQuestionDto;

namespace DeskJr.Service.Abstract
{
    public interface ISurveyQuestionService
    {
        public Task<bool> AddSurveyQuestionAsync(CreateServeyQuestionDto createSurveyQuestionDto);
        public Task<bool> UpdateSurveyQuestionAsync(SurveyQuestionDto surveyQuestionDto);
        public Task<bool> DeleteSurveyQuestionAsync(Guid id);
        public Task<List<SurveyQuestionDto>> GetAllSurveyQuestionAsync();
        public Task<SurveyQuestionDto> GetSurveyQuestionByIdAsync(Guid id);
        public Task<List<SurveyQuestionDto>> GetSurveyQuestionsBySurveyIdAsync(Guid surveyId);
    }
}
