using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract;

public interface ITeamRepository:IGenericRepository<Team>
{
    public IEnumerable<Team> GetListWithIncludeManagerAsync();
    public List<Team> GetSubTeamsById(Guid id);
}
