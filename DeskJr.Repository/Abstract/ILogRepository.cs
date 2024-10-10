using DeskJr.Entity.Models;

namespace DeskJr.Repository.Abstract;
public interface ILogRepository
{
    public IEnumerable<Log> GetAllLogsAsync();
}
