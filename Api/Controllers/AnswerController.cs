using Api.Commons;
using Api.Models;
using Api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AddAnswerRequest
    {
        public string QuestionId { get; set; }
        public string ExamId { get; set; }
        public string AnswerType { get; set; }
        public string Content { get; set; }
    }

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {

        private readonly IRequestUserService _requestUserService;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly IAnswerRepository _answerRepository;
        public AnswerController(IRequestUserService requestUserService, IMapper mapper, IDateTime dateTime, IAnswerRepository answerRepository)
        {
            _requestUserService = requestUserService;
            _mapper = mapper;
            _dateTime = dateTime;
            _answerRepository = answerRepository;
        }



        [HttpPost("AddAnswer")]
        public async Task<ActionResult> AddAnswer(AddAnswerRequest request)
        {
            var user = await _requestUserService.GetUser();
            try
            {
                var answer = await _answerRepository.GetSingle(x => x.QuestionId == request.QuestionId && x.CreatedById == user.Id && x.ExamId == request.ExamId);
                if(answer == null)
                {
                    answer = _mapper.Map<Answer>(request);
                    answer.UpdateCreatedByFields(user, _dateTime.NowUTC);
                    answer.UpdateModifiedByFields(user, _dateTime.NowUTC);
                    _answerRepository.Add(answer);
                    await _answerRepository.Commit();
                   
                }
                else
                {
                    answer.Content = request.Content;
                    answer.UpdateModifiedByFields(user, _dateTime.NowUTC);
                    _answerRepository.Update(answer);
                    await _answerRepository.Commit();
                }
                return Ok("Answer Saved Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
