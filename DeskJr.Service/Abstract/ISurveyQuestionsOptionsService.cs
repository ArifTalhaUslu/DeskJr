using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface ISurveyQuestionsOptionsService
    {
        public Task<bool> AddSurveyQuestionOptionsAsync(AddOrUpdateSurveyQuestionOptionsDto createSurveyQuestionOptionsDto);
        public Task<bool> UpdateSurveyQuestionOptionsAsync(AddOrUpdateSurveyQuestionOptionsDto surveyQuestionOptionsDto);
        public Task<bool> DeleteSurveyQuestionOptionsAsync(Guid id);
        public Task<List<SurveyQuestionOptionsDto>> GetAllSurveyQuestionOptionsAsync();
        public Task<SurveyQuestionOptionsDto> GetSurveyQuestionOptionsByIdAsync(Guid id);
        public Task<List<SurveyQuestionOptionsDto>> GetSurveyQuestionOptionsBySurveyQuestionAsync(Guid surveyQuestionId);
    }
}
