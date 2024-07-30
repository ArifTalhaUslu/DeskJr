using System.ComponentModel.DataAnnotations.Schema;
using DeskJr.Entity.Types;

namespace DeskJr.Entity.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public EnumRole EmployeeRole { get; set; }
        public EnumGender Gender { get; set; }

        [ForeignKey("EmployeeTitleId")]
        public Guid? EmployeeTitleId { get; set; }

        public EmployeeTitle? EmployeeTitle { get; set; }

        [ForeignKey("TeamId")]
        public Guid? TeamId { get; set; }

        public Team? Team { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public Employee()
        {
            EmployeeRole = EnumRole.Employee;
        }

    }
}
