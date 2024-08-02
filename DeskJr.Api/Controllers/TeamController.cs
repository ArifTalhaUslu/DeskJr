using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
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

        [HttpPut]
        public async Task<ActionResult> UpdateTeam(AddOrUpdateTeamDto teamDto)
        {
            var result = await _teamService.UpdateTeamAsync(teamDto);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
