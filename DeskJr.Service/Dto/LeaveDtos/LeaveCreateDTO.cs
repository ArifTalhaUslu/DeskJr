using System;
using DeskJr.Entity.Types;

namespace DeskJr.Service.Dto
{
    public class LeaveCreateDTO
    {
        public Guid RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string RequestComments { get; set; }
        public EnumStatusOfLeave StatusOfLeave { get; set; }
        public Guid? ApprovedById { get; set; }
    }
}