using Api.Commons;
using Api.Enums;
using Api.Models;
using Api.Repositories;
using Api.Requests.CourseRequests;
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
        private readonly IRequestUserService _requestUserTenantService;
        public CourseController(
            ICourseRepository courseRepository, IDepartmentRepository departmentRepository, IRequestUserService requestUserTenantService)
        {

            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _requestUserTenantService = requestUserTenantService;
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
