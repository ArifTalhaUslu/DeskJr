using System;
namespace DeskJr.Service.Dto.TeamDtos
{
	public class TeamDto
	{
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid? ManagerId { get; set; }
    }
}

