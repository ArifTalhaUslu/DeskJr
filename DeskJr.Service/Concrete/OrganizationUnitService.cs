using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskJr.Service.Concrete
{
    public class OrganizationUnitService : IOrganizationUnitService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public OrganizationUnitService(ITeamRepository teamRepository, IEmployeeRepository employeeRepository)
        {
            _teamRepository = teamRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<OrganizationUnitDto>> GetOrganizationUnitsAsync()
        {
            // Veritabanından tüm takımları yöneticileriyle birlikte alıyoruz
            var teams = _teamRepository.GetListWithIncludeManagerAsync().ToList();
            // Kök takımları buluyoruz (UpTeamId'si null olanlar)
            var rootTeams = teams.Where(t => !t.UpTeamId.HasValue).ToList();

            var organizationUnits = new List<OrganizationUnitDto>();

            // Her kök takım için bir DTO oluşturuyoruz
            foreach (var rootTeam in rootTeams)
            {
                var employees = _employeeRepository.GetEmployeesByTeamId(rootTeam.ID);

                var rootDto = BuildOrganizationUnit(rootTeam, teams);
                organizationUnits.Add(rootDto);
            }

            return organizationUnits;
        }

        private OrganizationUnitDto BuildOrganizationUnit(Team team, List<Team> allTeams)
        {
            var employees = _employeeRepository.GetEmployeesByTeamId(team.ID);
            // Takım için bir DTO oluşturuyoruz
            var teamDto = new OrganizationUnitDto
            {
                Id = team.ID,
                Label = team.Name,
                Type = "team",
                Expanded = true,
                Data = new PersonDataDto
                {
                    Name = team.Manager is not null ? team.Manager.Name : "-",
                    Base64Image = team.Manager?.Base64Image,
                    Employees = employees.Where(e => e.TeamId == team.ID)
                                            .Select(e => new SimplifiedEmployeeDto { Id = e.ID, Name = e.Name , Base64Image = e.Base64Image })
                                            .ToList()
                },
                Children = new List<OrganizationUnitDto>()
            };
            // Bu takımın altındaki takımları buluyoruz
            var childTeams = allTeams.Where(t => t.UpTeamId == team.ID).ToList();

            // Her alt takım için yine aynı işlemi gerçekleştiriyoruz
            foreach (var childTeam in childTeams)
            {
                var childDto = BuildOrganizationUnit(childTeam, allTeams);
                teamDto.Children.Add(childDto);
            }

            return teamDto;
        }
    }
}