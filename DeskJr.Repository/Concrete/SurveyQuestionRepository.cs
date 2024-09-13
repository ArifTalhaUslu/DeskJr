using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class SurveyQuestionRepository : GenericRepository<SurveyQuestion>, ISurveyQuestionRepository
    {
        private AppDbContext _context;

        public SurveyQuestionRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<SurveyQuestion>> GetSurveyQuestionsBySurveyIdAsync(Guid surveyId)
        {
            return await _context.SurveyQuestions.Include(x => x.Survey).Where(x => x.SurveyId == surveyId).ToListAsync(); 
        }
    }
}
