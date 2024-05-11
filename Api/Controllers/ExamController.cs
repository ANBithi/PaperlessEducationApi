using Api.Commons;
using Api.Models;
using Api.Repositories;
using Api.Requests;
using Api.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examMetadataRepository;
        private readonly IRequestUserService _requestUserService;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;


        public ExamController(IExamRepository examMetadataRepository,
            IRequestUserService requestUserService, IDateTime dateTime, IMapper mapper, IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _examMetadataRepository = examMetadataRepository;
            _requestUserService = requestUserService;
            _dateTime = dateTime;
            _mapper = mapper;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        [HttpPost("AddMetadata")]
        public async Task<ActionResult<string>> AddMetadata(AddExamMetadataRequest request)
        {
            var user = await _requestUserService.GetUser();
            try
            {
                var newMetadata = new Exam
                {
                    CourseName = request.CourseName,
                    CourseCode = request.CourseCode,
                    SectionNumber = request.SectionNumber,
                    SectionId = request.SectionId,
                    ExamTitle = request.ExamTitle,
                    AnswerType = request.AnswerType,
                    CountPercentile = request.CountPercentile,
                    Duration = request.Duration,
                    StartTime = DateTime.Parse(request.StartTime),
                    TotalMarks = request.TotalMarks,
                    ExamType = request.ExamType

                };

                newMetadata.UpdateCreatedByFields(user, _dateTime.NowUTC);
                newMetadata.UpdateModifiedByFields(user, _dateTime.NowUTC);
                _examMetadataRepository.Add(newMetadata);
                await _examMetadataRepository.Commit();
                return newMetadata.Id;

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPost("UpdateQuestion")]
        public async Task<ActionResult<bool>> UpdateQuestion(UpdateQuestionRequest request)
        {
            var user = await _requestUserService.GetUser();
            try
            {
                var ques = await _questionRepository.GetSingle(x => x.Id == request.Id);
                ques.Content = request.Content;
                ques.Mark = request.Mark;
                ques.QuestionType = request.QuestionType;
                ques.Options = request.Options;
                ques.UpdateModifiedByFields(user, _dateTime.NowUTC);
                _questionRepository.Update(ques);
                await _questionRepository.Commit();
                return Ok("Question Updated");
            }
            catch
            {
                return BadRequest("Question not found");
            }

        }

        [HttpPost("AddQuestion")]
        public async Task<ActionResult> AddQuestion(AddQuestionRequest request)
        {
            var user = await _requestUserService.GetUser();
            try
            {
                var ques = _mapper.Map<Question>(request);
                ques.UpdateCreatedByFields(user, _dateTime.NowUTC);
                ques.UpdateModifiedByFields(user, _dateTime.NowUTC);
                _questionRepository.Add(ques);
                await _questionRepository.Commit();
                return Ok("Question Added.");
            }
            catch
            {
                return BadRequest("Request failed.");
            }

        }

        [HttpGet("GetMetadata")]
        public async Task<ActionResult<ExamMetadataViewModel>> GetMetadata([FromQuery] string sectionId)
        {
            try
            {
                var examMeta = await _examMetadataRepository.GetSingle(x => x.SectionId == sectionId);

                if (examMeta != null)
                {

                    return _mapper.Map<ExamMetadataViewModel>(examMeta); ;
                }
                else
                {
                    return BadRequest("Exam not found");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("GetUpcomingExams")]
        public async Task<ActionResult<List<ExamMetadataViewModel>>> GetUpcomingExams([FromQuery] string sectionId)
        {
            try
            {
                var examMeta = await _examMetadataRepository.GetAllAsync(x => x.SectionId == sectionId && x.StartTime > _dateTime.NowUTC);
                var listOfExams = _mapper.Map<List<ExamMetadataViewModel>>(examMeta);

                return listOfExams;

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("GetAllExamsByUser")]
        public async Task<ActionResult<List<ExamMetadataViewModel>>> GetAllExamsByUser()
        {
            var user = await _requestUserService.GetUser();
            try
            {
                var examMeta = await _examMetadataRepository.GetAllAsync(x => x.CreatedById == user.Id);
                var listOfExams = _mapper.Map<List<ExamMetadataViewModel>>(examMeta);

                return listOfExams;

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpGet("GetQuestions")]
        public async Task<ActionResult<List<QuestionViewModel>>> GetQuestions([FromQuery] string examId, bool includeAnswers)
        {
            var user = await _requestUserService.GetUser();
            try
            {
                var questions = await _questionRepository.GetAllAsync(x => x.ExamId == examId);
                var questionsViewmodel = _mapper.Map<List<QuestionViewModel>>(questions);
                if (includeAnswers)
                {
                    foreach(var vm in questionsViewmodel)
                    {
                        var answer = await _answerRepository.GetSingle(x => x.QuestionId == vm.Id && x.CreatedById == user.Id && x.ExamId == examId);
                        vm.Answer = _mapper.Map<AnswerViewModel>(answer);
                    }
                   
                }

                return questionsViewmodel;

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }



}
