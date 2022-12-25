using Api.Models;
using Api.Repositories;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AddDepertmentRequest
    {
        public string Name { get; set; }
        public string CourseDistribution { get; set; }
        public int TotalCredits { get; set; }
        public int MinimumCreditPerSem { get; set; }

    }
    public class AddFacultyRequest
    {
        public string BelongsTo { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }

    }
    public class AddStudentRequest
    {
        public string BelongsTo { get; set; }
        public string Department { get; set; }
        public string Advisor { get; set; }
        public string Batch { get; set; }
        public int AdmissionYear { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFacultyRepository _facultyRepository;
        public DepartmentController(IDepartmentRepository departmentRepository, 
            IInstituteRepository instituteRepository,
            IStudentRepository studentRepository,
            IUserRepository userRepository,
            IFacultyRepository facultyRepository)
        {
            _departmentRepository = departmentRepository;
            _instituteRepository = instituteRepository;
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _facultyRepository = facultyRepository;
        }


        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(AddDepertmentRequest request)
        {
            var institute = await _instituteRepository.GetSingle(x=> true);
                
            try
            {
                var department = new Department
                {
                    Name = request.Name,
                    CourseDistribution = request.CourseDistribution,
                    TotalCredits = request.TotalCredits,
                    MinimumCreditPerSem = request.MinimumCreditPerSem
                   
                };

                department.Courses = new List<string>();

                _departmentRepository.Add(department);
                await _departmentRepository.Commit();
                institute.Departments.Add(department.Id);
                _instituteRepository.Update(institute);
                await _instituteRepository.Commit();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
       
    }
}
