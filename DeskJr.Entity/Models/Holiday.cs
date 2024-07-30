namespace DeskJr.Entity.Models
{
    public class Holiday : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
