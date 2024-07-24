namespace DeskJr.Entity.Models
{
    public class LeaveAllocation : BaseEntity
    {

        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }

        // Navigation properties
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }

        public int Period { get; set; }

    }
}