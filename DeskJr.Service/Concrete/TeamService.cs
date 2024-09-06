using System;
using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Repository.Concrete;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Service.Concrete
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public TeamService(ITeamRepository teamRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AddOrUpdateTeamAsync(AddOrUpdateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            var allTeams = await _teamRepository.GetAllAsync();
            var baseTeamExist = allTeams.Any(x => x.UpTeamId is null);

            if (teamDto.ID == null)
            {
                if (team.UpTeamId is null)
                {
                    if (baseTeamExist)
                    {
                        throw new InvalidOperationException("Sadece bir base unit olabilir.");
                    }
                }
                return await _teamRepository.AddAsync(team);
            }
            if (!teamDto.UpTeamId.HasValue && baseTeamExist)
            {
                throw new InvalidOperationException("Sadece bir base unit olabilir.");
            }

            return await _teamRepository.UpdateAsync(team);
        }

        public async Task<bool> DeleteTeamAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new NotFoundException("No team exists with the provided identifier.");
            }

            var subTeams = _teamRepository.GetSubTeamsById(id);

            if (subTeams.Count > 0)
            {
                throw new InvalidOperationException("This team is referenced by other teams as their UpTeam. Please delete Sub Teams first.");
            }
            var employees = await _employeeRepository.GetByTeamIdAsync(id);
            if (employees.Count > 0)
            {
                throw new InvalidOperationException("Cannot delete the team because it has associated employees. Please reassign or remove employees first.");
            }


            return await _teamRepository.DeleteAsync(id);
        }

        public async Task<List<TeamDto>> GetAllTeamsAsync()
        {
            var teams = _teamRepository.GetListWithIncludeManagerAsync();
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

        public async Task<TeamDto?> GetUpTeamByIdAsync(Guid upTeamId)
        {
            var team = await _teamRepository.GetByIdAsync(upTeamId);
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

