using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;

        public SurveyQuestionService(ISurveyQuestionRepository surveyQuestionRepository, ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddOrUpdateSurveyQuestionAsync(AddOrUpdateServeyQuestionDto surveyQuestionDto)
        {
            if (surveyQuestionDto.Id == null)
            {
                if (string.IsNullOrEmpty(surveyQuestionDto.Text))
                    throw new BadRequestException("Text is not null field!");

                return await _surveyQuestionRepository.AddAsync(_mapper.Map<SurveyQuestion>(surveyQuestionDto));
            }

            var questionFromDb = await _surveyQuestionRepository.GetByIdAsync(surveyQuestionDto.Id.Value);
            surveyQuestionDto.SurveyId = questionFromDb?.SurveyId;

            return await _surveyQuestionRepository.UpdateAsync(_mapper.Map<SurveyQuestion>(surveyQuestionDto));
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
    }
}
