using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.TeamDtos;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeam(CreateTeamDto teamDto)
        {
            var result = await _teamService.AddTeamAsync(teamDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(Guid id)
        {
            var result = await _teamService.DeleteTeamAsync(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeamById(Guid id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            return Ok(team);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeam(UpdateTeamDto teamDto)
        {
            var result = await _teamService.UpdateTeamAsync(teamDto);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
