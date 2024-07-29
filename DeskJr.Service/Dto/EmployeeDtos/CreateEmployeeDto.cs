using DeskJr.Entity.Types;
using System.ComponentModel.DataAnnotations;

namespace DeskJr.Service.Dto
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public DateOnly DayOfBirth { get; set; }
        public EnumGender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GuidFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && Guid.TryParse(value.ToString(), out _))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("The field must be a valid GUID.");
        }
    }
}
