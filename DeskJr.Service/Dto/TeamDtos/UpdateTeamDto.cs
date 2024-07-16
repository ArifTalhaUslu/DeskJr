using System;
namespace DeskJr.Service.Dto.TeamDtos
{
	public class UpdateTeamDto
	{
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid? ManagerId { get; set; }
    }
}

