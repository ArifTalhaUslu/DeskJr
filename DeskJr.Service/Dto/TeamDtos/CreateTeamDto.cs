using System;
namespace DeskJr.Service.Dto
{
    public class CreateTeamDto
    {
        public string Name { get; set; }
        public Guid? ManagerId { get; set; }
    }
}

