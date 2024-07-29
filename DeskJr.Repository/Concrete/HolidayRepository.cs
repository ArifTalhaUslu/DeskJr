using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;

namespace DeskJr.Repository.Concrete
{
    public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
    {
        private readonly AppDbContext _context;

        public HolidayRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
