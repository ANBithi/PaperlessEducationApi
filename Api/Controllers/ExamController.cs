using Api.Commons;
using Api.Models;
using Api.Models.Exam;
using Api.Repositories;
using Api.Requests.Exam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamMetadataRepository _examMetadataRepository;
        private readonly IRequestUserService _requestUserService;
        private readonly IDateTime _dateTime;


        public ExamController(IExamMetadataRepository examMetadataRepository,
            IRequestUserService requestUserService, IDateTime dateTime)
        {
            _examMetadataRepository = examMetadataRepository;
            _requestUserService = requestUserService;
            _dateTime = dateTime;
        }

        [HttpPost("AddMetadata")]
        public async Task<ActionResult<bool>> AddMetadata(AddExamMetadataRequest request)
        {
            var user = await _requestUserService.GetUser();
            try
            {
                var newMetadata = new ExamMetadata
                {
                    CourseName = request.CourseName,
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
                return true;

            }
            catch
            {
                return false;
            }

        }
    }
}
