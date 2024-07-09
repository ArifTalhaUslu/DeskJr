using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Entity.Models
{
    public class EmployeeTitle
    {
        public Guid ID { get; set; }
        public string TitleName { get; set; }

        public EmployeeTitle()
        {
            ID = Guid.NewGuid();
        }
    }
}
