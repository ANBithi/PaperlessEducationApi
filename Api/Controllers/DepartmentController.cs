using Api.Models;
using Api.Repositories;
using Api.Requests.DepertmentRequests;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public DepartmentController(IDepartmentRepository departmentRepository, 
            IInstituteRepository instituteRepository)
        {
            _departmentRepository = departmentRepository;
            _instituteRepository = instituteRepository;
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
