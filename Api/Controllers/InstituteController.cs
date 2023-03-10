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
                instituteModel.Departments = new List<string>();
                _instituteRepository.Add(instituteModel);
                await _instituteRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }

        }
        //[HttpPost("addHoliday")]
        //public async Task<ActionResult<bool>> AddHoilday(DateTime[] dates)
        //{
        //    try
        //    {
                //var holidaysString = new List<string>();
                //foreach (DateTime date in dates)
                //{
                //    var holiday = date.ToString("MM-dd-yyyy");
                //    holidaysString.Add(holiday);
                //}


                //var institute = await _instituteRepository.GetSingle(x => x.Name == "Growth");
                //institute.Holidays.AddRange(holidaysString);
                //_instituteRepository.Update(institute);
                //await _instituteRepository.Commit();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //[HttpGet("getHolidays")]
        //public async Task<ActionResult<List<string>>> GetHolidays()
        //{
        //var institute = await _instituteRepository.GetSingle(x=> x.Name == "Growth");
        //return institute.Holidays;
        //}
    }
}
