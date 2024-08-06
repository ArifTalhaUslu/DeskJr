using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _holidayRepository;
        private readonly IMapper _mapper;

        public HolidayService(IHolidayRepository holidayRepository, IMapper mapper)
        {
            _holidayRepository = holidayRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddOrUpdateHolidayAsync(AddOrUpdateHolidayDto holidayDto)
        {
            if (holidayDto.ID == null)
            {
                if (string.IsNullOrEmpty(holidayDto.Name))
                    throw new BadRequestException("Name is not null field!");

                return await _holidayRepository.AddAsync(_mapper.Map<Holiday>(holidayDto));
            }

            return await _holidayRepository.UpdateAsync(_mapper.Map<Holiday>(holidayDto));
        }

        public async Task<bool> DeleteHolidayAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No holiday exists with the provided identifier.");
            }

            return await _holidayRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<HolidayDto>> GetAllHolidaysAsync()
        {
            var holidays = await _holidayRepository.GetAllAsync();
            if (holidays == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<IEnumerable<HolidayDto>>(holidays);
        }

        public async Task<HolidayDto?> GetHolidayByIdAsync(Guid id)
        {
            var holiday = await _holidayRepository.GetByIdAsync(id);
            if (holiday == null)
            {
                throw new NotFoundException("No holiday exists with the provided identifier.");
            }
            return _mapper.Map<HolidayDto>(holiday);
        }
    }
}
