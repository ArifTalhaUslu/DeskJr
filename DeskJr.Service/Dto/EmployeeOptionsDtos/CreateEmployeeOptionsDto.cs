using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Dto
{
    public class CreateEmployeeOptionsDto
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OptionId { get; set; }
    }
}
