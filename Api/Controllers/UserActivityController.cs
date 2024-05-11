using Api.Attributes;
using Api.Commons;
using Api.Models;
using Api.Repositories;
using Api.Requests;
using Api.Requests.NotificationRequests;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class UserActivityViewModel
    {
        public ActiveStatusEnum Status { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UserActivityController : ControllerBase
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public UserActivityController(IUserActivityRepository userActivityRepository, IMapper mapper, IUserRepository userRepository, IDateTime dateTime)
        {
            _userActivityRepository = userActivityRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _dateTime = dateTime;
        }

        [LiveUpdateServiceValidate]
        [HttpPost("add")]
        public async Task<bool> Add(AddActivityRequest request)
        {
            var user = await _userRepository.GetSingle(x => x.Id == request.UserId);
            if(user == null)
            {
                return false;
            }
            var foundActivity = await _userActivityRepository.GetSingle(x => x.CreatedById == request.UserId && x.ActivityForId == request.ActivityForId);
            if (foundActivity == null)
            {
                foundActivity = _mapper.Map<UserActivity>(request);
                foundActivity.UpdateCreatedByFields(user, _dateTime.NowUTC);
                foundActivity.UpdateModifiedByFields(user, _dateTime.NowUTC);
                _userActivityRepository.Add(foundActivity);
            }
            else
            {
                foundActivity.Status = request.Status;
                foundActivity.UpdateModifiedByFields(user, _dateTime.NowUTC);
                _userActivityRepository.Update(foundActivity);
            }

            var notify = new NotificationRequest
            {
                Target = "exam",
                Type = $"{(int)request.Status}",
                DataId = request.ActivityForId,
                CreatedById = request.UserId,

            };
            await _userActivityRepository.Commit();
            var notifcationHelper = new NotificationHelper();
            notifcationHelper.Notify(notify);
            return true;
        }

        [Authorize]
        [HttpGet("getSingleById")]
        public async Task<ActionResult<ActiveStatusEnum>> GSetSingleById([FromQuery]string givenId, string activityForId)
        {
            var foundActivity = await _userActivityRepository.GetSingle(x => x.CreatedById == givenId && x.ActivityForId == activityForId);
            if (foundActivity == null)
            {
                return ActiveStatusEnum.Offline;
            }
            else
            {
                return foundActivity.Status;
            }
        }


    }
}
