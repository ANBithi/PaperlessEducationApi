using Api.Attributes;
using Api.Commons;
using Api.Enums;
using Api.Models;
using Api.Repositories;
using Api.Requests;
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
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRequestUserService _requestUserService;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        public CourseController(
            ICourseRepository courseRepository, IDepartmentRepository departmentRepository, IRequestUserService requestUserService, IMapper mapper, IDateTime dateTime, IStudentRepository studentRepository)
        {

            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _requestUserService = requestUserService;
            _mapper = mapper;
            _dateTime = dateTime;
            _studentRepository = studentRepository;
        }

        [UserTypeValidator(userType:UserTypeEnum.Admin)]
        [HttpPost("Add")]
        public async Task<ActionResult> Add(AddCourseRequest request)
        {
            var depertment = await _departmentRepository.GetSingle(x => x.Id == request.DepartmentId);
            request.Code = $"{depertment.Abbreviation.ToUpper()}-{request.Code}";
            var user =  await _requestUserService.GetUser();
            try
            {
                var course = _mapper.Map<Course>(request);
                _courseRepository.Add(course);
                course.UpdateCreatedByFields(user, _dateTime.NowUTC);
                course.UpdateModifiedByFields(user, _dateTime.NowUTC);
                course.CourseCover = "";
               if(request.LessonType == LessonType.Theory)
                {
                    course.Credits = 3;
                }
               else 
                {
                    course.Credits = 1;
                }
                await _courseRepository.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        [HttpGet("getAllCourses")]
        public async Task<ActionResult<List<CourseViewModel>>> GetAllCourses([FromQuery] string departmentId)
        {
            var courses = await _courseRepository.GetAllAsync(x => x.DepartmentId == departmentId);

            var responseList = new List<CourseViewModel>();


            foreach (Course cor in courses)
            {
                CourseViewModel view = new CourseViewModel
                {
                    Id = cor.Id,
                    Name = cor.Name,
                    CourseCode = cor.Code
                };
                responseList.Add(view);
            }


            return responseList;
        }


        [HttpGet("getAllCoursesByStudent")]
        public async Task<ActionResult<List<CourseViewModel>>> GetAllCoursesByStudent()
        {
            var user = await _requestUserService.GetUser();
            var student = await _studentRepository.GetSingle(x => x.BelongsTo == user.Id);
            if(student == null)
            {
                return BadRequest("Invalied Request");
            }

            var courses = await _courseRepository.GetAllAsync(x => x.DepartmentId == student.DepartmentId);

            var responseList = new List<CourseViewModel>();


            foreach (Course cor in courses)
            {
                CourseViewModel view = new CourseViewModel
                {
                    Id = cor.Id,
                    Name = cor.Name,
                    CourseCode = cor.Code,
                    CourseDetails = cor.CourseDetails
                };
                responseList.Add(view);
            }


            return responseList;
        }

    }

    public class CourseViewModel
    {
        public string Name { get; set; }
        public string CourseDetails { get; set; }
        public string CourseCode { get; set; }
        public string Id { get; set; }
    }
}
