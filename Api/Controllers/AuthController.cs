using Api.IServices;
using Api.Models;
using Api.Requests.UserRequests;
using Api.Responses.UserResponses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<object>> Login(LoginRequest request)
        {
            var loginStatus = await _userService.LogInUser(request.Email, request.Password);
            AuthData authData = null;
            if(loginStatus.IsAuthorized)
            {
                authData = _authService.GetAuthData(loginStatus.User);
            }

            return new
            {
                success = loginStatus.IsAuthorized,
                authData,
                message = loginStatus.Message
            }; ;
        }
    }

   
}
