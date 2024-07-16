using System;


using System.Collections.Generic;

namespace DeskJr.Entity.Models
{
    public class LeaveType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; }

        // Navigation properties
        public ICollection<LeaveAllocation> LeaveAllocations { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
        public LeaveType()
        {
            Id = Guid.NewGuid();
        }
    }
}
