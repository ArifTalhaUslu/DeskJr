using System;
using DeskJr.Entity.Models;
using DeskJr.Entity.Types;

namespace DeskJr.Service.Dto
{
    public class LeaveCreateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public string RequestComments { get; set; }
        public EnumStatusOfLeave StatusOfLeave { get; set; }
        public Guid? ApprovedById { get; set; }
    }
}