using System;
namespace DeskJr.Service.Dto
{
	public class AddOrUpdateTeamDto
	{
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? UpTeamId { get; set; }
    }
}

