using System;
using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

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

        public async Task<bool> AddOrUpdateTeamAsync(AddOrUpdateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            if (teamDto.ID == null)
            {
                return await _teamRepository.AddAsync(team);
            }

            return await _teamRepository.UpdateAsync(team);

        }

        public async Task<bool> DeleteTeamAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No team exists with the provided identifier.");
            }

            return await _teamRepository.DeleteAsync(id);
        }

        public async Task<List<TeamDto>> GetAllTeamsAsync()
        {
            var teams =  _teamRepository.GetListWithIncludeManagerAsync();
            if (teams == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<List<TeamDto>>(teams);
        }

        public async Task<TeamDto?> GetTeamByIdAsync(Guid id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (id == null)
            {
                throw new NotFoundException("No team exists with the provided identifier.");
            }

            return _mapper.Map<TeamDto>(team);
        }

        public async Task<bool> UpdateTeamAsync(AddOrUpdateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            if (team == null)
            {
                throw new NotFoundException("No team exists with the provided identifier.");
            }

            return await _teamRepository.UpdateAsync(team);
        }
    }
}

