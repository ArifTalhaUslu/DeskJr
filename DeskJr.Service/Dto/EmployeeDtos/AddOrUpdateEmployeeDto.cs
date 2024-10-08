using DeskJr.Entity.Types;

namespace DeskJr.Service.Dto
{
    public class AddOrUpdateEmployeeDto
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public EnumRole EmployeeRole { get; set; }
        public EnumGender Gender { get; set; }
        public Guid? EmployeeTitleId { get; set; }
        public Guid? TeamId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Base64Image { get; set; }
    }
}
