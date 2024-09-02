using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Dto
{
    public  class UpdateMultipleSettingDto
    {
        public List<AddOrUpdateSettingDto> Settings { get; set; }
    }
}
