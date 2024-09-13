
using DeskJr.Entity.Models;
using DeskJr.Service.Dto;
using DeskJr.Service.Dto.SurveyDto;

namespace DeskJr.Service.Abstract
{
    public interface ISurveyService
    {
        public Task<bool> AddSurveyAsync(CreateSurveyDto createSurveyDto);
        public Task<bool> UpdateSurveyAsync(SurveyDto surveyDto);
        public Task<bool> DeleteSurveyAsync(Guid id);
        public Task<List<SurveyDto>> GetAllSurveyAsync();
        public Task<SurveyDto> GetSurveyByIdAsync(Guid id);

    }
}
