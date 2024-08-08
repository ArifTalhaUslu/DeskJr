using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repositories.Concrete;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
    public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
    {
        private readonly AppDbContext _context;

        public HolidayRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Holiday>> GetUpcomingHolidaysAsync()
        {
            var now = DateTime.Now;
            return await _context.Holidays
                .Where(x => x.EndDate >= now && x.StartDate <= now.AddMonths(1))
                .ToListAsync();
        }
    }
}
