using DeskJr.Entity.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Dto  
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        public EnumGender Gender { get; set; }
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
