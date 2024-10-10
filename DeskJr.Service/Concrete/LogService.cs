using AutoMapper;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.LoggerDtos;

namespace DeskJr.Service.Concrete
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;
        public LogService(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public IEnumerable<LogDto> GetAllLogsAsync()
        {
            var logs = _logRepository.GetAllLogsAsync();
            return _mapper.Map<IEnumerable<LogDto>>(logs);
        }
    }
}
