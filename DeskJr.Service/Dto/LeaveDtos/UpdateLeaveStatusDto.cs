
using DeskJr.Entity.Models;
using DeskJr.Entity.Types;

namespace DeskJr.Service.Dto
{
    public class UpdateLeaveStatusDto
    {
        public Guid LeaveId { get; set; }
        public EnumStatusOfLeave NewStatus { get; set; }
        public EmployeeDto? ApprovedBy { get; set; }
    }
}
