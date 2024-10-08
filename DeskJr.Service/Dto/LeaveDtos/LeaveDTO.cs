using System;
using DeskJr.Entity.Types;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Dto
{
    public class LeaveDTO
    {
        public Guid ID { get; set; }
        public Guid RequestingEmployeeId { get; set; }
        public EmployeeDto RequestingEmployee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid LeaveTypeId { get; set; }
        public LeaveTypeDTO LeaveType { get; set; }
        public string RequestComments { get; set; }
        public EnumStatusOfLeave StatusOfLeave { get; set; }
        public Guid? ApprovedById { get; set; }
        public EmployeeDto ApprovedBy { get; set; }
    }
}

