using Api.Enums;
using Api.Models;
using Api.Repositories;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Api.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AddCourseRequest
    {
        public string BelongsTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Department { get; set; }
        public string CourseDetails { get; set; }
        public int Type { get; set; }
        public List<string> PreRequisities { get; set; }
        public int Credits { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IEmployeeInfoRepository _infoRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public CourseController(IEmployeeInfoRepository infoRepository, 
            ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
            _infoRepository = infoRepository;
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(AddCourseRequest request)
        {
            var department = await _departmentRepository.GetById(request.Department);
           try
            {
                var course = new Course
                {
                    BelongsTo = request.BelongsTo,
                    Name = request.Name,
                    Code = request.Code,
                    Department = request.Department,
                    CourseDetails = request.CourseDetails,
                    Type = (CourseTypeEnum)request.Type,
                    PreRequisities = request.PreRequisities,
                    Credits = request.Credits
                };

                course.Sections = new List<string>();
                course.AddedAt = DateTime.Now;
           
                _courseRepository.Add(course);
                await _courseRepository.Commit();
                department.Courses.Add(course.Id);
                _departmentRepository.Update(department);
                await _departmentRepository.Commit();
                return true;

            }
            catch
            {
                return false;
            }
           
        }
    }
}
