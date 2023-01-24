using Api.IServices;
using Api.Requests.StudentRequests;
using Api.Requests.UserRequests;
using Api.Responses.UserResponses;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }


        [HttpPost("Create")]
        public async Task<ActionResult<UserAddStatus>> CreateUser(CreateUserRequest request)
        {
            var userAddStatus = await _userService.CreateUser(request);

            return userAddStatus;
        }

        [HttpPost("AddStudents")]
        public async Task<ActionResult<UserAddStatus>> AddStudents(AddStudentsRequest request)
        {
            var userAddStatus = await _userService.AddStudents(request);

            return userAddStatus;
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult<PasswordChangeStatus>> ChangePassword(ChangePasswordRequest request)
        {
            var passwordStatus = await _userService.ChangePassword(request.OldPassword, request.NewPassword, request.Id);

            return passwordStatus;
        }

        [HttpPost("UserInteraction")]
        public async Task<ActionResult<bool>> UpdateUserInteraction(UserInteractionRequest request)
        {
                var response = await _userService.UpdateUserInteraction(request.CreatedById, request.Type);
                return response;
        }
    }
}
