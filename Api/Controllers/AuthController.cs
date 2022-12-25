using Api.Models;
using Api.Repositories;
using Api.Services;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class LoginRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class CheckAndUpdateRequest
    {
        public string Id { get; set; }
    }
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

        [HttpPost("CheckAndUpdate")]
        public async Task<ActionResult<bool>> CheckAndUpdate(CheckAndUpdateRequest request)
        {
            var userProfile = await _userService.CheckAndUpdateProfileStatus(request.Id);
            return userProfile;
        }

    }
}
