using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract
{
    public interface IHolidayRepository : IGenericRepository<Holiday>
    {

        public Task<IEnumerable<Holiday>> GetUpcomingHolidaysAsync();
    }
}
