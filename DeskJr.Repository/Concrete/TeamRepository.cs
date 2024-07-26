using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;

namespace DeskJr.Repository.Concrete
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

