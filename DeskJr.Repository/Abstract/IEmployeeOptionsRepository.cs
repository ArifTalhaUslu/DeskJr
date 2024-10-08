using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
    public interface IEmployeeOptionsRepository : IGenericRepository<EmployeeOptions>
    {
        public Task<bool> EmployeeSurveyStatusAsync(Guid userId, Guid surveyId);
        public Task<List<EmployeeOptions>> GetByOptionId(Guid optionId);
    }
}
