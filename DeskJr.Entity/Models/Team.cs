using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DeskJr.Entity.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("ManagerId")]
        public Guid? ManagerId { get; set; }

        [JsonIgnore]
        public Employee? Manager { get; set; }
    }

}
