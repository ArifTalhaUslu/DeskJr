using System.ComponentModel.DataAnnotations;

namespace DeskJr.Entity.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}