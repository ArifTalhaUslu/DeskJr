using DeskJr.Service.Dto.SurveyQuestionOptionsDto;

namespace DeskJr.Service.Abstract
{
    public interface ISurveyQuestionsOptionsService
    {
        public Task<bool> AddSurveyQuestionOptionsAsync(CreateSurveyQuestionOptionsDto createSurveyQuestionOptionsDto);
        public Task<bool> UpdateSurveyQuestionOptionsAsync(SurveyQuestionOptionsDto surveyQuestionOptionsDto);
        public Task<bool> DeleteSurveyQuestionOptionsAsync(Guid id);
        public Task<List<SurveyQuestionOptionsDto>> GetAllSurveyQuestionOptionsAsync();
        public Task<SurveyQuestionOptionsDto> GetSurveyQuestionOptionsByIdAsync(Guid id);
        public Task<List<SurveyQuestionOptionsDto>> GetSurveyQuestionOptionsBySurveyQuestionAsync(Guid surveyQuestionId);
    }
}
