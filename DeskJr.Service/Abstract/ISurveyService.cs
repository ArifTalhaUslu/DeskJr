
using DeskJr.Service.Dto.SurveyDto;

namespace DeskJr.Service.Abstract
{
    public interface ISurveyService
    {
        public Task<bool> AddOrUpdateSurveyAsync(AddOrUpdateSurveyDto surveyDto);
        public Task<bool> DeleteSurveyAsync(Guid id);
        public Task<List<SurveyDto>> GetAllSurveyAsync();
        public Task<SurveyDto> GetSurveyByIdAsync(Guid id);

    }
}
