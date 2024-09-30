using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class SurveyRepository : GenericRepository<Survey> , ISurveyRepository
    {
        private readonly AppDbContext _context; 
        public SurveyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Survey>> GetAllElementSurveyAsync(Guid id)
        {
            return await _context.Surveys.Where(x => x.ID == id)
                .Include(x => x.SurveyQuestions)
                .ThenInclude(x => x.SurveyQuestionOptions)
                .ToListAsync();
        }
    }
}
