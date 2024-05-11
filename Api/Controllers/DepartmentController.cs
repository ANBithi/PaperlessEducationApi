using Api.Attributes;
using Api.Commons;
using Api.Enums;
using Api.Models;
using Api.Repositories;
using Api.Requests;
using Api.ViewModels;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IRequestUserService _requestUserService;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        public DepartmentController(IDepartmentRepository departmentRepository,
            IInstituteRepository instituteRepository, IRequestUserService requestUserService, IMapper mapper, IDateTime dateTime, IFacultyRepository facultyRepository)
        {
            _departmentRepository = departmentRepository;
            _instituteRepository = instituteRepository;
            _requestUserService = requestUserService;
            _mapper = mapper;
            _dateTime = dateTime;
            _facultyRepository = facultyRepository;
        }

        [UserTypeValidator(userType: UserTypeEnum.Admin)]
        [HttpPost("Add")]
        public async Task<ActionResult> Add(AddDepertmentRequest request)
        {
            var user = await _requestUserService.GetUser();
            try
            {
                if(await _departmentRepository.GetSingle(x => x.DepartmentCode == request.DepartmentCode) != null)
                {
                    return BadRequest("Department exist with same code");
                }
                if (await _departmentRepository.GetSingle(x => x.Name == request.Name) != null)
                {
                    return BadRequest("Department exist with same name");
                }
                if (await _departmentRepository.GetSingle(x => x.Abbreviation == request.Abbreviation) != null)
                {
                    return BadRequest("Department exist with same abbreviation");
                }


                var department = new Department
                {
                    Name = request.Name,
                    CourseDistribution = request.CourseDistribution,
                    TotalCredits = 140,
                    MinimumCreditPerSem = 3,
                    Abbreviation = request.Abbreviation.ToLower(),
                    DepartmentCode = request.DepartmentCode,
                   
                };

                department.UpdateCreatedByFields(user, _dateTime.NowUTC);
                department.UpdateModifiedByFields(user, _dateTime.NowUTC);
               
                _departmentRepository.Add(department);
                await _departmentRepository.Commit();
            }
            catch (Exception ex)
            {
                throw ex ;
            }

            return Ok();
        }

        [HttpGet("getDepartments")]
        public async Task<ActionResult<List<DepartmentViewModel>>> GetDepartements()
        {
            var departments = await _departmentRepository.GetAllAsync(x => true);

            var responseList = new List<DepartmentViewModel>();


            foreach (Department dep in departments)
            {
                DepartmentViewModel view = new DepartmentViewModel
                {
                    Id = dep.Id,
                    Name = dep.Name,
                    Abbreviation = dep.Abbreviation
                };
                responseList.Add(view);
            }


            return responseList;
        }

        [HttpGet("getFacultiesByDepartment")]
        public async Task<ActionResult<List<FacultyViewModel>>> GetFacultiesByDepartment([FromQuery] string departmentId)
        {
            if (departmentId == null)
            {
                return NotFound();
            }
            var faculties = await _facultyRepository.GetAllAsync(x => x.Department == departmentId);

            var responseList = new List<FacultyViewModel>();


            foreach (Faculty fac in faculties)
            {
                FacultyViewModel view = new FacultyViewModel
                {
                    Id = fac.Id,
                    Name = fac.Name,
                    UserId = fac.BelongsTo,
                };
                responseList.Add(view);
            }


            return responseList;
        }
    }
    public class DepartmentViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Abbreviation { get; set; }
    }
}
