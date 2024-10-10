using DeskJr.Service.Dto.LoggerDtos;

namespace DeskJr.Service.Abstract
{
    public interface ILogService
    {
        public IEnumerable<LogDto> GetAllLogsAsync();
    }
}
