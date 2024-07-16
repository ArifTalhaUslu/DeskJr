using System;
using AutoMapper;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.EmployeeDtos;
using DeskJr.Service.Dto.TeamDtos;

namespace DeskJr.Service.Concrete
{
	public class TeamService: ITeamService
	{
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddTeamAsync(CreateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            return await _teamRepository.AddAsync(team);
        }

        public async Task<bool> DeleteTeamAsync(Guid id)
        {
            return await _teamRepository.DeleteAsync(id);
        }

        public async Task<List<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return _mapper.Map<List<TeamDto>>(teams);
        }

        public async Task<TeamDto?> GetTeamByIdAsync(Guid id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<bool> UpdateTeamAsync(UpdateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            return await _teamRepository.UpdateAsync(team);
        }
    }
}

