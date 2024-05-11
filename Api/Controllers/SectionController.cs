using Api.Commons;
using Api.Enums;
using Api.IServices;
using Api.Models;
using Api.Repositories;
using Api.Requests.SectionRequests;
using Api.Responses.SectionResponses;
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
        private readonly ISectionRepository _sectionRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IRequestUserService _requestUserService;
        private readonly IUserService _userService;
        public SectionController(IFacultyRepository facultyRepository,
            ISectionRepository sectionRepository,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository, IResultRepository resultRepository, IRequestUserService requestUserService, IUserService userService)
        {
            _courseRepository = courseRepository;
            _sectionRepository = sectionRepository;
            _facultyRepository = facultyRepository;
            _studentRepository = studentRepository;
            _resultRepository = resultRepository;
            _requestUserService = requestUserService;
            _userService = userService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(AddSectionRequest request)
        {
            try
            {
                var course = await _courseRepository.GetById(request.BelongsTo);
                var section = new Section
                {
                    CourseId = request.BelongsTo,
                    SectionNumber = request.SectionNumber,
                    MaxAllocation = request.MaxAllocation,
                    Faculty = request.Faculty,
                    SectionStartTime = request.SectionStartTime,
                    SectionEndTime = request.SectionEndTime
                };
                section.Students = new List<string>();
                

                _sectionRepository.Add(section);
                await _sectionRepository.Commit();
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
                return true;
            }
            catch
            {
                return false;
            }
        }




        [HttpGet("getAllSections")]
        public async Task<ActionResult<SectionResponse>> GetAllSections()
        {
            var user = await _requestUserService.GetUser();
            
            var response = new SectionResponse
            {
                Data = new List<SectionViewModel>(),
                Response = false
            };
            if(user.UserType == UserTypeEnum.Faculty)
            {
                var faculty = await _facultyRepository.GetSingle(x => x.BelongsTo == user.Id);

                var sections = await _sectionRepository.GetAllAsync(x => x.Faculty == faculty.Id);

                if (sections.Count == 0)
                {
                    return response;
                }
                foreach (Section sec in sections)
                {
                    var course = await _courseRepository.GetById(sec.CourseId);
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
                if(student == null)
                {
                    return response;
                }
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
                    var course = await _courseRepository.GetById(sec.CourseId);
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

            return response;
        }


        [HttpGet("getAllSectionsByFaculty")]
        public async Task<ActionResult<SectionResponse>> GetAllSectionsByFaculty()
        {
            var user = await _requestUserService.GetUser();

            var response = new SectionResponse
            {
                Data = new List<SectionViewModel>(),
                Response = false
            };
            var faculty = await _facultyRepository.GetSingle(x => x.BelongsTo == user.Id);

            var sections = await _sectionRepository.GetAllAsync(x => x.Faculty == faculty.Id);

            if (sections.Count == 0)
            {
                return response;
            }
            foreach (Section sec in sections)
            {
                var course = await _courseRepository.GetById(sec.CourseId);
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

            return response;
        }

        [HttpGet("GetAllSectionsByStudent")]
        public async Task<ActionResult<SectionResponse>> GetAllSectionsByStudent()
        {
            var user = await _requestUserService.GetUser();

            var response = new SectionResponse
            {
                Data = new List<SectionViewModel>(),
                Response = false
            };
                var student = await _studentRepository.GetSingle(x => x.BelongsTo == user.Id);
                var sections = new List<Section>();
                if (student == null)
                {
                    return response;
                }
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
                    var course = await _courseRepository.GetById(sec.CourseId);
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
                var course = await _courseRepository.GetById(section.CourseId);
                var studentsView = new List<StudentViewModel>();
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

        [HttpGet("getStudentDetails")]
        public async Task<ActionResult<StudentProfileViewModel>> GetStudentDetails([FromQuery] string studentId)
        {
            var student = await _studentRepository.GetSingle(x => x.Id == studentId);
            var response = await _userService.GetStudentProfile(student);
            return response;
        }
    }
}
