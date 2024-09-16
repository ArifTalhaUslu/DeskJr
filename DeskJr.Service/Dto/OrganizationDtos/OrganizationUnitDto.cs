using DeskJr.Entity.Types;
using DeskJr.Service.Dto.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskJr.Service.Dto
{
    public class OrganizationUnitDto
    {
        public Guid Id { get; set; }
        public string Label { get; set; } 
        public string Type { get; set; }  
        public bool Expanded { get; set; } 
        public PersonDataDto Data { get; set; }
        public List<OrganizationUnitDto> Children { get; set; } = new();
    }

    public class PersonDataDto
    {
        public string Name { get; set; }
        public string Base64Image { get; set; }
        public List<SimplifiedEmployeeDto> Employees { get; set; } = new();

    }
    public class SimplifiedEmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Base64Image { get; set; }
    }
}
