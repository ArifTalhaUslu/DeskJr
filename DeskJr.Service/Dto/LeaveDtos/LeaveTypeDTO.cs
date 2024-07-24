using System;
namespace DeskJr.Service.Dto
{
    public class LeaveTypeDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

