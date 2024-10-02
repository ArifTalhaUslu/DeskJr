using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class SurveyService : ISurveyService

    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;
        public SurveyService(ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddOrUpdateSurveyAsync(AddOrUpdateSurveyDto surveyDto)
        {
            if (surveyDto.Id == null)
            {
                if (string.IsNullOrEmpty(surveyDto.Name))
                    throw new BadRequestException("Name is not null field!");

                return await _surveyRepository.AddAsync(_mapper.Map<Survey>(surveyDto));
            }

            return await _surveyRepository.UpdateAsync(_mapper.Map<Survey>(surveyDto));
        }

        public async Task<bool> DeleteSurveyAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No Survey exists with the provided identifier.");
            }

            return await _surveyRepository.DeleteAsync(id);
        }

        public async Task<List<SurveyDto>> GetAllSurveyAsync()
        {
            var surveys = await _surveyRepository.GetAllAsync();
            if (surveys == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<List<SurveyDto>>(surveys);
        }

        public async Task<IEnumerable<SurveyDto>> GetAllElementSurveyAsync(Guid id)
        {
            if(id == null)
            {
                throw new NotFoundException("No Survey exists with the provided identifier.");
            }

            var survey = await _surveyRepository.GetAllElementSurveyAsync(id);

            return _mapper.Map<IEnumerable<SurveyDto>>(survey);


        }

        public async Task<SurveyDto> GetSurveyByIdAsync(Guid id)
        {
            var survey = await _surveyRepository.GetByIdAsync(id);
            if (id == null)
            {
                throw new NotFoundException("No Survey exist with the provided identifier. ");
            }

            return _mapper.Map<SurveyDto>(survey);
        }
    }
}
