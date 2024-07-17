﻿using System;


using System.Collections.Generic;

namespace DeskJr.Entity.Models
{
    public class LeaveType: BaseEntity
    {
        
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; }

        // Navigation properties
        public ICollection<LeaveAllocation> LeaveAllocations { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
      
    }
}
