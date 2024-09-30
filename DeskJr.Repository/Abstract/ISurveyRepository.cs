using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
    public interface ISurveyRepository : IGenericRepository<Survey>
    {
        public Task<IEnumerable<Survey>> GetAllElementSurveyAsync(Guid id);
    }
}
