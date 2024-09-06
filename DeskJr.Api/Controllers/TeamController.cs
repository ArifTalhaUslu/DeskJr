using DeskJr.Common.Exceptions;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateTeam(AddOrUpdateTeamDto teamDto)
        {
            var result = await _teamService.AddOrUpdateTeamAsync(teamDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid team identifier.");
            }

            var result = await _teamService.DeleteTeamAsync(id);

            return Ok();
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

        [HttpGet("upTeamsById/{upTeamId}")]
        public async Task<ActionResult> GetUpTeamById(Guid upTeamId)
        {
            var team = await _teamService.GetUpTeamByIdAsync(upTeamId);

            return Ok(team);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTeam(AddOrUpdateTeamDto teamDto)
        {
            var result = await _teamService.UpdateTeamAsync(teamDto);

            return Ok();
        }
    }
}
