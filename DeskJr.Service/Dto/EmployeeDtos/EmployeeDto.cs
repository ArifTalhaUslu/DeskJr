using DeskJr.Entity.Types;

namespace DeskJr.Service.Dto.EmployeeDtos
{
    public class EmployeeDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public EnumRole EmployeeRole { get; set; }
        public EnumGender Gender { get; set; }
        public Guid? TitleId { get; set; }
        public Guid? TeamId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
