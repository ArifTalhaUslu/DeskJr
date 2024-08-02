namespace DeskJr.Entity.Models
{
    public class Team : BaseEntity
    {

        public string Name { get; set; }
        public Guid? ManagerId { get; set; }
        public Employee Manager { get; set; }


    }

}
