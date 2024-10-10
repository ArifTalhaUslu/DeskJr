using AutoMapper;
using DeskJr.Common.Exceptions;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dto;

namespace DeskJr.Service.Concrete
{
    public class EmployeeOptionsService : IEmployeeOptionsService
    {
        private readonly IEmployeeOptionsRepository _employeeOptionRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly ISurveyQuestionOptionsRepository _surveyQuestionOptionsRepository;
        private readonly IMapper _mapper;
        public EmployeeOptionsService(IEmployeeOptionsRepository employeeOptionsRepository, ISurveyRepository surveyRepository, ISurveyQuestionRepository surveyQuestionRepository, ISurveyQuestionOptionsRepository surveyQuestionOptionsRepository, IMapper mapper)
        {
            _employeeOptionRepository = employeeOptionsRepository;
            _surveyRepository = surveyRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _surveyQuestionOptionsRepository = surveyQuestionOptionsRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddOrUpdateEmployeeOptionsAsync(CreateEmployeeOptionsDto employeeOptions)
        {

            if (employeeOptions.Id == null)
            {
                return await _employeeOptionRepository.AddAsync(_mapper.Map<EmployeeOptions>(employeeOptions));
            }

            return await _employeeOptionRepository.UpdateAsync(_mapper.Map<EmployeeOptions>(employeeOptions));
        }

        public async Task<bool> AddRangeAsync(List<CreateEmployeeOptionsDto> createEmployeeOptionsDtos)
        {
            return await _employeeOptionRepository.AddRangeAsync(_mapper.Map<List<EmployeeOptions>>(createEmployeeOptionsDtos));
        }

        public async Task<bool> DeleteEmployeeOptionsAsync(Guid id)
        {
            if (id == null)
            {
                throw new NotFoundException("No EmployeeOptions exists with the provided identifier.");
            }

            return await _employeeOptionRepository.DeleteAsync(id);
        }

        public async Task<bool> EmployeeSurveyStatus(Guid userId, Guid surveyId)
        {
            if (userId == null && surveyId == null)
            {
                throw new NotFoundException("No record exists with the provided identifier.");
            }

            return await _employeeOptionRepository.EmployeeSurveyStatusAsync(userId, surveyId);
        }

        public async Task<IEnumerable<EmployeeOptionsDto>> GetAllEmployeeOptionsAsync()
        {
            var employeeOptions = await _employeeOptionRepository.GetAllAsync();
            if (employeeOptions == null)
            {
                throw new Exception("The requested operation could not be completed.");
            }

            return _mapper.Map<IEnumerable<EmployeeOptionsDto>>(employeeOptions);
        }

        public async Task<EmployeeOptionsDto?> GetEmployeeOptionsByIdAsync(Guid id)
        {
            var employeeOptions = await _employeeOptionRepository.GetByIdAsync(id);
            if (employeeOptions == null)
            {
                throw new NotFoundException("No EmployeeOptions exists with the provided identifier.");
            }

            return _mapper.Map<EmployeeOptionsDto>(employeeOptions);
        }

        public async Task<SurveyResultDto?> GetSurveyResultsAsync(Guid surveyId)
        {
            var survey = await _surveyRepository.GetByIdWithInclude(surveyId);

            if (survey is null)
            {
                return null;
            }

            var resultDto = new SurveyResultDto();
            resultDto.Id = survey.ID;
            resultDto.Name = survey.Name;

            foreach (var question in survey.SurveyQuestions)
            {
                var optionlist = new List<OptionResultDto>();
                foreach (var option in question.SurveyQuestionOptions)
                {
                    var employeeOptions = await _employeeOptionRepository.GetByOptionId(option.ID);
                    var optionSelectCount = employeeOptions.Count;
                    optionlist.Add(new OptionResultDto() { AnswerCount = optionSelectCount, Id = option.ID, Text = option.Text });
                }
                resultDto.Questions.Add(new QuestionResultDto() { Options = optionlist, Text = question.Text, });
            }

            return resultDto;
        }
    }
}
