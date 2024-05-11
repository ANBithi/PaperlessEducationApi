using Api.Commons;
using Api.CustomValues;
using Api.Models;
using Api.Repositories;
using Api.Requests.ResultRequests;
using Api.Responses.ResultResponses;
using Api.ViewModels;
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
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;
        private readonly IStudentRepository _studentRespository;
        private readonly IRequestUserService _requestUserService;
        private readonly IDateTime _dateTime;

        public ResultController(IResultRepository resultRepository, IStudentRepository studentRepository, IRequestUserService requestUserService, IDateTime dateTime)
        {
            _resultRepository = resultRepository;
            _studentRespository = studentRepository;
            _requestUserService = requestUserService;
            _dateTime = dateTime;
        }



        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(AddResultRequest request)
        {

            var user = await _requestUserService.GetUser();
            try
            {
                var result = new Result
                {
                    BelongsTo = request.BelongsTo,
                    QuizMark = request.QuizMark,
                    MidMark = request.MidMark,
                    FinalMark = request.FinalMark,
                    AssignmentMark = request.AssignmentMark,
                    ProjectMark = request.ProjectMark,
                    AttendanceMark = request.AttendanceMark,
                    CourseCode = request.CourseCode,
                    CourseName = request.CourseName,

                };
                var totalMarks = request.QuizMark + request.MidMark +
                    request.ProjectMark + request.FinalMark + request.AttendanceMark + request.AssignmentMark;
                var finalGrade = ConstructResult(totalMarks);
                result.Grade = finalGrade.Grade;
                result.Gpa = finalGrade.Gpa;
                result.UpdateCreatedByFields(user, _dateTime.NowUTC);
                result.UpdateModifiedByFields(user, _dateTime.NowUTC);
                _resultRepository.Add(result);
                await _resultRepository.Commit();

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private GradeResult ConstructResult(float totalMark)
        {
            var result = new GradeResult
            {
                Grade = "F",
                Gpa = 0.00
            };

            if (totalMark >= 80)
            {
                result.Grade = "A";
                result.Gpa = 4.00;
            }
            else if (totalMark >= 70)
            {
                result.Grade = "A-";
                result.Gpa = 3.50;
            }
            else if (totalMark >= 60)
            {
                result.Grade = "B+";
                result.Gpa = 3.25;
            }
            else if (totalMark >= 55)
            {
                result.Grade = "B";
                result.Gpa = 3.00;
            }
            else if (totalMark >= 50)
            {
                result.Grade = "B-";
                result.Gpa = 2.75;
            }
            else if (totalMark >= 45)
            {
                result.Grade = "C+";
                result.Gpa = 2.50;
            }
            else if (totalMark >= 40)
            {
                result.Grade = "C";
                result.Gpa = 2.25;
            }
            else if (totalMark >= 40)
            {
                result.Grade = "C-";
                result.Gpa = 2.00;
            }
            else if (totalMark >= 35)
            {
                result.Grade = "D";
                result.Gpa = 1.75;
            }
            else
            {
                result.Grade = "F";
                result.Gpa = 0.00;
            }
            return result;
        }


        [HttpGet("getSingle")]
        public async Task<ActionResult<ResultViewModel>> GetSingle(string belongsTo, string courseCode)
        {
            var resultModel = new ResultViewModel();
            try
            {
                var student = await _studentRespository.GetSingle(x => x.BelongsTo == belongsTo);
                var result = await _resultRepository.GetSingle(x => x.BelongsTo == student.Id && x.CourseCode == courseCode);
                    var r = new ResultViewModel
                    {
                      CourseCode = result.CourseCode,
                      CourseName = result.CourseName,
                      Gpa = result.Gpa,
                      Grade =result.Grade,
                      AssignmentMark = result.AssignmentMark,
                      QuizMark = result.QuizMark,
                      MidMark =result.MidMark,
                      FinalMark = result.FinalMark,
                      AttendanceMark =result.AttendanceMark,
                      ProjectMark = result.ProjectMark,
                    };
                resultModel = r;
                return resultModel;
            }
            catch (Exception e)
            {
                return resultModel;
            }

        }


        [HttpGet("getAll")]
        public async Task<ActionResult<ResultResponse>> GetAll()
        {
            var user = await _requestUserService.GetUser();
            var resultResponse = new ResultResponse
            {
                Response = false,
                Data = new List<ResultViewModel>()
            };
            try
            {
                var student = await _studentRespository.GetSingle(x => x.BelongsTo == user.Id);
                var results = await _resultRepository.GetAllAsync(x => x.BelongsTo == student.Id);
                foreach (Result r in results)
                {
                    var resultModel = new ResultViewModel
                    {
                        CourseCode = r.CourseCode,
                        CourseName = r.CourseName,
                        Gpa = r.Gpa,
                        Grade = r.Grade
                    };
                    resultResponse.Data.Add(resultModel);
                }
                resultResponse.Response = true;
                return resultResponse;
            }
            catch (Exception e)
            {
                return resultResponse;
            }

        }

    }
}
