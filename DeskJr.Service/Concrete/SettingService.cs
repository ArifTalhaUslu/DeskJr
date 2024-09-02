using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;

        public SettingService(ISettingRepository settingRepository, IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddOrUpdateSettingAsync(AddOrUpdateSettingDto settingDto)
        {
            if (settingDto.ID == null)
            {
                if (string.IsNullOrEmpty(settingDto.Key))
                    throw new BadRequestException("Key is a required field!");

                return await _settingRepository.AddAsync(_mapper.Map<Setting>(settingDto));
            }

            return await _settingRepository.UpdateAsync(_mapper.Map<Setting>(settingDto));
        }

        public async Task<bool> DeleteSettingAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new NotFoundException("No setting exists with the provided identifier.");
            }

            return await _settingRepository.DeleteAsync(id);
        }


        public async Task<IEnumerable<SettingDto>> GetAllSettingAsync()
        {
            var settings = await _settingRepository.GetAllAsync();
            if (settings == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<IEnumerable<SettingDto>>(settings);
        }



        public async Task<SettingDto?> GetSettingByIdAsync(Guid id)
        {
            var setting = await _settingRepository.GetByIdAsync(id);
            if (setting == null)
            {
                throw new NotFoundException("No setting exists with the provided identifier.");
            }
            return _mapper.Map<SettingDto>(setting);
        }

    }
}
