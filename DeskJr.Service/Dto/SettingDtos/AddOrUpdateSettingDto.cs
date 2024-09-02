using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Dto
{
    public class AddOrUpdateSettingDto
    {
        public Guid ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
