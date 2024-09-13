using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;

namespace DeskJr.Repository.Concrete
{
    public class SurveyRepository : GenericRepository<Survey> , ISurveyRepository
    {
        private readonly AppDbContext _context; 
        public SurveyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
