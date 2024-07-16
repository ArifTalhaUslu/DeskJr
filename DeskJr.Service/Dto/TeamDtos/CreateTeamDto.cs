using System;
namespace DeskJr.Service.Dto.TeamDtos
{
	public class CreateTeamDto
	{
        public string Name { get; set; }
        public Guid? ManagerId { get; set; }
    }
}

