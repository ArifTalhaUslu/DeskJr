using DeskJr.Entity.Models;
using DeskJr.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Abstract
{
    public interface ISettingService
    {
        public Task<bool> AddOrUpdateSettingAsync(AddOrUpdateSettingDto settingDto);
        public Task<bool> DeleteSettingAsync(Guid id);
        public Task<IEnumerable<SettingDto>> GetAllSettingAsync();
        public Task<SettingDto?> GetSettingByIdAsync(Guid id);
        public Task<SettingDto?> GetAccuredDayAsync();

    }
}
