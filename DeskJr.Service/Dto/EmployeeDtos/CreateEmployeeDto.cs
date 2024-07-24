using DeskJr.Entity.Types;

namespace DeskJr.Service.Dto.EmployeeDtos
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public EnumGender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
