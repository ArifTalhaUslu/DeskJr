using System.ComponentModel.DataAnnotations;

namespace DeskJr.Entity.Models
{
    public class EmployeeTitle : BaseEntity
    {

        [Required]

        public string TitleName { get; set; }


    }
}
