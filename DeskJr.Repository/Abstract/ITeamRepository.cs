using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract;

public interface ITeamRepository
{
    public Task<bool> AddAsync(Team team);
    public Task<bool> UpdateAsync(Team team);
    public Task<bool> DeleteAsync(Guid id);
    public Task<List<Team>> GetAllAsync();
    public Task<Team?> GetByIdAsync(Guid id);

}
