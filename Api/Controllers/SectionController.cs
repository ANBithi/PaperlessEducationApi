using Api.Commons;
using Api.Enums;
using Api.Models;
using Api.Repositories;
using Api.Requests.SectionRequests;
using Api.Responses.SectionResponses;
using Api.Security;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IRequestUserService _requestUserTenantService;
        public SectionController(IFacultyRepository facultyRepository,
            IUserRepository userRepository,
            ISectionRepository sectionRepository,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository, IResultRepository resultRepository, IRequestUserService requestUserTenantService)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _sectionRepository = sectionRepository;
            _facultyRepository = facultyRepository;
            _studentRepository = studentRepository;
            _resultRepository = resultRepository;
            _requestUserTenantService = requestUserTenantService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(AddSectionRequest request)
        {
            try
            {
                var course = await _courseRepository.GetById(request.BelongsTo);
                var section = new Section
                {
                    BelongsTo = request.BelongsTo,
                    SectionNumber = request.SectionNumber,
                    MaxAllocation = request.MaxAllocation,
                    Faculty = request.Faculty,
                    SectionStartTime = request.SectionStartTime,
                    SectionEndTime = request.SectionEndTime
                };
                section.Students = new List<string>();
                

                _sectionRepository.Add(section);
                await _sectionRepository.Commit();
                course.Sections.Add(section.Id);
                _courseRepository.Update(course);
                await _courseRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("addStudent")]
        public async Task<ActionResult<bool>> AddStudent(SectionStudentRequest request)
        {
            try
            {
                var section = await _sectionRepository.GetById(request.SectionId);
                section.Students.Add(request.StudentId);
                var student = await _studentRepository.GetById(request.StudentId);
                student.CurrentClasses.Add(request.SectionId);
                _sectionRepository.Update(section);
                _studentRepository.Update(student);
                await _sectionRepository.Commit();
                await _studentRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }




        [HttpGet("getAllSections")]
        public async Task<ActionResult<SectionResponse>> GetAllSections([FromQuery]string userId, int type)
        {
            var user = await _requestUserTenantService.GetUser();
            
            var response = new SectionResponse
            {
                Data = new List<SectionViewModel>(),
                Response = false
            };
            if((UserTypeEnum)type == UserTypeEnum.Faculty)
            {
                var faculty = await _facultyRepository.GetSingle(x => x.BelongsTo == user.Id);

                var sections = await _sectionRepository.GetAllAsync(x => x.Faculty == faculty.Id);

                if (sections.Count == 0)
                {
                    return response;
                }
                foreach (Section sec in sections)
                {
                    var course = await _courseRepository.GetById(sec.BelongsTo);
                    var sectionView = new SectionViewModel
                    {
                        SectionId = sec.Id,
                        CourseName = course.Name,
                        CourseCode = course.Code,
                        SectionNumber = sec.SectionNumber,
                        StartTime = sec.SectionStartTime,
                        EndTime = sec.SectionEndTime,
                        CourseDescription = course.CourseDetails,
                        CourseCover = course.CourseCover
                    };
                    response.Data.Add(sectionView);
                }
                response.Response = true;
              
            }
            else
            {
                var student = await _studentRepository.GetSingle(x => x.BelongsTo == user.Id);
                var sections = new List<Section>();
                foreach (string id in student.CurrentClasses)
                {
                    var sec = await _sectionRepository.GetById(id);
                    sections.Add(sec);
                }

                if (sections.Count == 0)
                {
                    return response;
                }
                foreach (Section sec in sections)
                {
                    var course = await _courseRepository.GetById(sec.BelongsTo);
                    var sectionView = new SectionViewModel
                    {
                        SectionId = sec.Id,
                        CourseName = course.Name,
                        CourseCode = course.Code,
                        SectionNumber = sec.SectionNumber,
                        StartTime = sec.SectionStartTime,
                        EndTime = sec.SectionEndTime,
                        CourseDescription = course.CourseDetails
                    };
                    response.Data.Add(sectionView);
                }
                response.Response = true;

            }

            return response;
        }

        [HttpGet("sectionDetail")]
        public async Task<ActionResult<SectionDetailResponse>> GetSectionDetail([FromQuery] string sectionId)
        {
            var sectionDetail = new SectionDetailResponse
            {
                Response = false,
                Data = new SectionViewModel(),
            };

            try
            {
                var section = await _sectionRepository.GetById(sectionId);
                var course = await _courseRepository.GetById(section.BelongsTo);
                var studentsView = new List<StudenViewModel>();
                foreach(string id in section.Students)
                {
                    Student std = await _studentRepository.GetById(id);
                    var result = await _resultRepository.GetSingle(x => x.BelongsTo == id && x.CourseCode == course.Code);
                    var stdView = new StudenViewModel
                    {
                        StudentId = std.StudentId,
                        Name = std.Name,
                        Id = std.Id,
                    };
                    if(result != null)
                    {
                        stdView.Result = new ResultViewModel
                        {
                            CourseCode = result.CourseCode,
                            CourseName = result.CourseName,
                            Gpa = result.Gpa,
                            Grade = result.Grade,
                        };
                    }
                    studentsView.Add(stdView);
                }

                var sectionView = new SectionViewModel
                {
                    SectionId = section.Id,
                    CourseName = course.Name,
                    CourseCode = course.Code,
                    SectionNumber = section.SectionNumber,
                    StartTime = section.SectionStartTime,
                    EndTime = section.SectionEndTime,
                    CourseDescription = course.CourseDetails,
                    Students = studentsView,
                    CourseCover = course.CourseCover
                };
                sectionDetail.Data = sectionView;
                sectionDetail.Response = true;
                return sectionDetail;
            }
            catch
            {
                return sectionDetail;
            }
           
        }


    }
}
