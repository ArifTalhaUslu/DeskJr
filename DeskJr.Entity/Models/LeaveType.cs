namespace DeskJr.Entity.Models
{
    public class LeaveType : BaseEntity
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; }
    }
}