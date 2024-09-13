
using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class SurveyQuestionOptionsRepository : GenericRepository<SurveyQuestionOptions> , ISurveyQuestionOptionsRepository
    {
        private readonly AppDbContext _context;
        public SurveyQuestionOptionsRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<SurveyQuestion>> GetSurveyQuestionOptionsBySurveyQuestionsAsync(Guid surveyQuestionsId)
        {
            return await _context.SurveyQuestionOptions.Where(x => x.SurveyQuestionId == surveyQuestionsId).Select(x => x.SurveyQuestion).ToListAsync(); 
        }


    }
}
