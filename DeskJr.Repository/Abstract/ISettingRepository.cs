using DeskJr.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Repository.Abstract
{
    public interface ISettingRepository : IGenericRepository<Setting>
    {
        public Task<Setting> GetAccuredDay();
    }
}
