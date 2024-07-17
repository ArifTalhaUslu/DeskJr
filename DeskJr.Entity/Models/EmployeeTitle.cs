using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Entity.Models
{
    public class EmployeeTitle: BaseEntity
    {
       
        [Required]
        
        public string TitleName { get; set; }

       
    }
}
