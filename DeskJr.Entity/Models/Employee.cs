using DeskJr.Entity.Types;

namespace DeskJr.Entity.Models
{
    public class Employee
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public EnumRole EmployeeRole { get; set; }
        public EnumGender Gender { get; set; }

        public Guid? TitleId { get; set; }
        public EmployeeTitle Title { get; set; }

        public Guid? TeamId { get; set; }
        public Team Team { get; set; }

        public Employee()
        {
            ID = Guid.NewGuid();
        }

    }
}
