using Api.Models;
using Api.Repositories;
using Api.Services;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class ChangePasswordRequest
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

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

        [HttpGet("GetSupervisors")]
        public async Task<ActionResult<List<SupervisorViewModel>>> GetSupervisors(string id)
        {
            var allUsers = await _userService.GetSupervisors(id);

            return allUsers;
        }



        [HttpGet("GetSupervisorById")]
        public async Task<ActionResult<SupervisorViewModel>> GetSupervisorById([FromQuery]string superId)
        {
            var supervisor = await _userService.GetSupervisorById(superId);

            return supervisor;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<SupervisorViewModel>>> GetAllUsers([FromQuery] string superId)
        {
            var allUsers = await _userService.GetAllUsers();

            return allUsers;
        }

    }
}
