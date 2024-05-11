using Api.IServices;
using Api.Requests.StudentRequests;
using Api.Requests.UserRequests;
using Api.Responses.UserResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Create")]
        public async Task<ActionResult> CreateUser(CreateUserRequest request)
        {

            await _userService.CreateUser(request);

                return Ok("Successfully created.");
        }

        [HttpPost("AddStudents")]
        public async Task<ActionResult<UserAddStatus>> AddStudents(AddStudentsRequest request)
        {
            await _userService.AddStudents(request);

           return Ok("Students added.");
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult<PasswordChangeStatus>> ChangePassword(ChangePasswordRequest request)
        {
            var passwordStatus = await _userService.ChangePassword(request.NewPassword, request.Otp);

            return passwordStatus;
        }

        [HttpPost("UserInteraction")]
        public async Task<ActionResult<bool>> UpdateUserInteraction(UserInteractionRequest request)
        {
                var response = await _userService.UpdateUserInteraction(request.Type);
                return response;
        }

        [HttpGet("userProfile")]
        public async Task<ActionResult> UserProfile()
        {
            var response = await _userService.UserProfile();
            return Ok(response);
        }


        [HttpGet("initiateChangePassword")]
        public async Task<InitiatePasswordChangeResponse> InitiateChangePassword()
        {
            var response = new InitiatePasswordChangeResponse
            {
                Url = "",
            Response = false,
            };
             response.Url = await _userService.InititateChangePassword();
            response.Response = true;
            return response;
        }
    }


    public class InitiatePasswordChangeResponse
    {
        public string Url { get; set; }
        public bool Response { get; set; }
    }
}
