using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class EmployeeOptionsRepository : GenericRepository<EmployeeOptions> , IEmployeeOptionsRepository
    {
        private readonly AppDbContext _context;
        public EmployeeOptionsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> EmployeeSurveyStatusAsync(Guid userId, Guid surveyId)
        {
            return await _context.EmployeeOptions
                .AnyAsync(eo => eo.UserId == userId
                                && _context.SurveyQuestions
                                    .Any(sq => sq.SurveyId == surveyId
                                               && sq.SurveyQuestionOptions
                                                   .Any(sqo => sqo.ID == eo.OptionId)));
        }

        public async Task<List<EmployeeOptions>> GetByOptionId(Guid optionId) 
        {
            return await _context.EmployeeOptions
                .Where(eo => eo.OptionId == optionId)
                .ToListAsync();
        }
    }
}
