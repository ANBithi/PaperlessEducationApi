using Api.IServices;
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

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginStatus>> Login(LoginRequest request)
        {
            var loginStatus = await _userService.LogInUser(request.Email, request.Password);                      
            return loginStatus;
        }

    }
}
