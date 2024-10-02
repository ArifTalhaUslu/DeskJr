using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface IEmployeeOptionsService
    {
        public Task<bool> AddOrUpdateEmployeeOptionsAsync(CreateEmployeeOptionsDto employeeOptions);
        public Task<bool> DeleteEmployeeOptionsAsync(Guid id);
        public Task<IEnumerable<EmployeeOptionsDto>> GetAllEmployeeOptionsAsync();
        public Task<EmployeeOptionsDto?> GetEmployeeOptionsByIdAsync(Guid id);
        public Task<bool> AddRangeAsync(List<CreateEmployeeOptionsDto> createEmployeeOptionsDtos);
        public Task<bool> EmployeeSurveyStatus(Guid userId, Guid surveyId);
        public Task<SurveyResultDto?> GetSurveyResultsAsync(Guid surveyId);

    }
}
