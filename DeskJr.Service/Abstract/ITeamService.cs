using System;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Abstract
{
    public interface ITeamService
    {
        public Task<bool> AddOrUpdateTeamAsync(AddOrUpdateTeamDto TeamDto);
        public Task<bool> UpdateTeamAsync(AddOrUpdateTeamDto TeamDto);
        public Task<bool> DeleteTeamAsync(Guid id);
        public Task<List<TeamDto>> GetAllTeamsAsync();
        public Task<TeamDto?> GetTeamByIdAsync(Guid id);
    }
}

