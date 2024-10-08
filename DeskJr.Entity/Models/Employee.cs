using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DeskJr.Entity.Types;

namespace DeskJr.Entity.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public EnumRole EmployeeRole { get; set; }
        public EnumGender Gender { get; set; }

        [ForeignKey("EmployeeTitleId")]
        public Guid? EmployeeTitleId { get; set; }
        [JsonIgnore]
        public EmployeeTitle? EmployeeTitle { get; set; }

        [ForeignKey("TeamId")]
        public Guid? TeamId { get; set; }
        [JsonIgnore]
        public Team? Team { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Base64Image { get; set; }


        public Employee()
        {
            EmployeeRole = EnumRole.Employee;
        }

    }
}
