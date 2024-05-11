using Api.Models;
using Api.Repositories;
using Api.Requests.InstituteRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InstituteController : ControllerBase
    {

        private readonly IInstituteRepository _instituteRepository;
    
        public InstituteController(IInstituteRepository instituteRepository)
        {
            _instituteRepository = instituteRepository;
        }
        [HttpPost("addInstitute")]
        public async Task<ActionResult<bool>> AddInstitute(InstituteModelRequest request)
        {
            try
            {
                var instituteModel = new Institute
                {
                    Name = request.Name,
                    Type = request.Type,
                    Address = request.Address,
                    Domain = request.Domain,
                    EstablishedYear = request.EstablishedYear,
                    About = request.About,
                    SemesterDuration = request.SemesterDuration,
                    Contact = request.Contact,
                };
                instituteModel.Holidays = new List<string>();
                _instituteRepository.Add(instituteModel);
                await _instituteRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }


   
}
