using DeskJr.Entity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Dto
{
    public class ManagerDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public EnumRole EmployeeRole { get; set; }
        public EnumGender Gender { get; set; }
        public Guid? EmployeeTitleId { get; set; }
        public EmployeeTitleDto EmployeeTitle { get; set; }
        public Guid? TeamId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
