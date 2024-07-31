using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface IHolidayService
    {
        public Task<bool> AddOrUpdateHolidayAsync(AddOrUpdateHolidayDto holidayDto);
        public Task<bool> DeleteHolidayAsync(Guid id);
        public Task<IEnumerable<HolidayDto>> GetAllHolidaysAsync();
        public Task<HolidayDto?> GetHolidayByIdAsync(Guid id);
    }
}
