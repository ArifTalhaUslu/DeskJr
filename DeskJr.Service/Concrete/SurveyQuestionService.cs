using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto.SurveyQuestionDto;

namespace DeskJr.Service.Concrete
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly IMapper _mapper;

        public SurveyQuestionService(ISurveyQuestionRepository surveyQuestionRepository, IMapper mapper)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddSurveyQuestionAsync(CreateServeyQuestionDto createSurveyQuestionDto)
        {
             return await _surveyQuestionRepository.AddAsync(_mapper.Map<SurveyQuestion>(createSurveyQuestionDto));
        }

        public async Task<bool> DeleteSurveyQuestionAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No SurveyQuestion exists with the provided identifier.");
            }

            return await _surveyQuestionRepository.DeleteAsync(id);
        }

        public async Task<List<SurveyQuestionDto>> GetAllSurveyQuestionAsync()
        {
            var surveyQuestions = await _surveyQuestionRepository.GetAllAsync();
            if (surveyQuestions == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<List<SurveyQuestionDto>>(surveyQuestions);
        }

        public async Task<List<SurveyQuestionDto>> GetSurveyQuestionsBySurveyIdAsync(Guid surveyId)
        {
            if(surveyId == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            var surveyQuestions = await _surveyQuestionRepository.GetSurveyQuestionsBySurveyIdAsync(surveyId);

            return _mapper.Map<List<SurveyQuestionDto>>(surveyQuestions);

        }

        public async Task<SurveyQuestionDto> GetSurveyQuestionByIdAsync(Guid id)
        {
            var surveyQuestion = await _surveyQuestionRepository.GetByIdAsync(id);
            if (id == null)
            {
                throw new NotFoundException("No SurveyQuestion exist with the provided identifier. ");
            }

            return _mapper.Map<SurveyQuestionDto>(surveyQuestion); 
        }

        public async Task<bool> UpdateSurveyQuestionAsync(SurveyQuestionDto surveyQuestionDto)
        {
            var surveyQuestion = _mapper.Map<SurveyQuestion>(surveyQuestionDto);
            if (surveyQuestion == null)
            {
                throw new NotFoundException("No SurveyQuestion exists with the provided identifier.");
            }

            return await _surveyQuestionRepository.UpdateAsync(surveyQuestion);
        }
    }
}
