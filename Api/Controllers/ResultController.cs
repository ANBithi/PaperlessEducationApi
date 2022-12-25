using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repositories;
using Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AddResultRequest
    {
        public string BelongsTo { get; set; }
        public float QuizMark { get; set; } = 0;
        public float MidMark { get; set; } = 0;
        public float ProjectMark { get; set; } = 0;
        public float AttendanceMark { get; set; } = 0;
        public float AssignmentMark { get; set; } = 0;
        public float FinalMark { get; set; } = 0;
        public string CourseName { get; set; }
        public string CourseCode { get; set; }

    }

    public class ResultResponse
    {
        public List<ResultViewModel> Data { get; set; }
        public bool Response { get; set; }
    }
    public class GradeResult
    {
        public string Grade { get; set; }
        public double Gpa { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;
        private readonly IStudentRepository _studentRespository;

        public ResultController(IResultRepository resultRepository, IStudentRepository studentRepository)
        {
            _resultRepository = resultRepository;
            _studentRespository = studentRepository;
        }



        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(AddResultRequest request)
        {

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


        [HttpGet("getAll")]
        public async Task<ActionResult<ResultResponse>> GetAll(string belongsTo)
        {
            var resultResponse = new ResultResponse
            {
                Response = false,
                Data = new List<ResultViewModel>()
            };
            try
            {
                var student = await _studentRespository.GetSingle(x => x.BelongsTo == belongsTo);
                var results = await _resultRepository.GetAllAsync(x => x.BelongsTo == student.Id);
                foreach (Result r in results)
                {
                    var resultModel = new ResultViewModel
                    {
                      CourseCode = r.CourseCode,
                      CourseName = r.CourseName,
                      Gpa = r.Gpa,
                      Grade =r.Grade
                    };
                    resultResponse.Data.Add(resultModel);
                }
                resultResponse.Response = true;
                return resultResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return resultResponse;
            }

        }
    }
}
