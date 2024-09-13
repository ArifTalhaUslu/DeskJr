using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.SurveyQuestionDto;
using DeskJr.Service.Dto.SurveyQuestionOptionsDto;

namespace DeskJr.Service.Concrete
{
    public class SurveyQuestionOptionsService : ISurveyQuestionsOptionsService
    {
        private readonly ISurveyQuestionOptionsRepository _surveyQuestionOptionsRepository;
        private readonly IMapper _mapper;

        public SurveyQuestionOptionsService(ISurveyQuestionOptionsRepository surveyQuestionOptionsRepository, IMapper mapper)
        {
            _surveyQuestionOptionsRepository = surveyQuestionOptionsRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddSurveyQuestionOptionsAsync(CreateSurveyQuestionOptionsDto createSurveyQuestionOptionsDto)
        {
            return await _surveyQuestionOptionsRepository.AddAsync(_mapper.Map<SurveyQuestionOptions>(createSurveyQuestionOptionsDto));
        }

        public async Task<bool> DeleteSurveyQuestionOptionsAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No SurveyQuestionOption exists with the provided identifier.");
            }

            return await _surveyQuestionOptionsRepository.DeleteAsync(id);
        }

        public async Task<List<SurveyQuestionOptionsDto>> GetAllSurveyQuestionOptionsAsync()
        {
            var surveyQuestionOptions = await _surveyQuestionOptionsRepository.GetAllAsync();
            if (surveyQuestionOptions == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<List<SurveyQuestionOptionsDto>>(surveyQuestionOptions);
        }

        public async Task<SurveyQuestionOptionsDto> GetSurveyQuestionOptionsByIdAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No SurvyQuestionOption exists with the provided identifier. ");
            }

            var surveyQuestionOptions = await _surveyQuestionOptionsRepository.GetByIdAsync(id);
            return _mapper.Map<SurveyQuestionOptionsDto>(surveyQuestionOptions);
        }

        public async Task<List<SurveyQuestionOptionsDto>> GetSurveyQuestionOptionsBySurveyQuestionAsync(Guid surveyQuestionId)
        {
            if (surveyQuestionId == null)
            {
                throw new NotFoundException("No SurvyQuestionOption exists with the provided identifier. ");
            }

            var surveyQuestionOptions = await _surveyQuestionOptionsRepository.GetSurveyQuestionOptionsBySurveyQuestionsAsync(surveyQuestionId);
            return _mapper.Map<List<SurveyQuestionOptionsDto>>(surveyQuestionOptions);
        }

        public async Task<bool> UpdateSurveyQuestionOptionsAsync(SurveyQuestionOptionsDto surveyQuestionOptionsDto)
        {
            var surveyQuestionOptions = _mapper.Map<SurveyQuestionOptions>(surveyQuestionOptionsDto);
            if (surveyQuestionOptions == null)
            {
                throw new NotFoundException("No SurveyQuestionOptions exists with the provided identifier.");
            }

            return await _surveyQuestionOptionsRepository.UpdateAsync(surveyQuestionOptions);
        }
    }
}
