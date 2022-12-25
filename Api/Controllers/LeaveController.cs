using Api.Enums;
using Api.Models;
using Api.Repositories;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    
    public class ApplyResponse
    {
        public bool Response { get; set; }
        public string Message { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILeaveCountRepository _leaveCountRepository;
        public LeaveController(ILeaveRepository leaveRepository, IUserRepository userRepository, ILeaveCountRepository leaveCountRepository)
        {
            _leaveRepository = leaveRepository;
            _userRepository = userRepository;
            _leaveCountRepository = leaveCountRepository;
        }


        [HttpPost("Apply")]
        public async Task<ActionResult<ApplyResponse>> Apply(AddLeaveRequest request)
        {
            var applyResponse = new ApplyResponse
            {
                Response = false,
                Message = "Leave limit exceeds.",
            };

            var userLeaveCount = await _leaveCountRepository.GetSingle(x => x.BelongsTo == request.BelongsTo);
            if (userLeaveCount is null)
            {
                var count = new LeaveCount
                {
                    BelongsTo = request.BelongsTo
                };
                _leaveCountRepository.Add(count);
                await _leaveCountRepository.Commit();
            } 
            else
            {
                switch (request.LeaveType)
                {
                    case "Annual Leave":
                        {
                            if (userLeaveCount.LeftAnnualLeave - request.TotalDays < 0)
                            {
                                return applyResponse;
                            }
                            break;
                        }

                    case "Sick Leave":
                        {
                            if (userLeaveCount.LeftSickLeave - request.TotalDays < 0)
                            {
                                return applyResponse;
                            }
                            break;
                        }
                    case "Child Sick Leave":
                        {
                            if (userLeaveCount.LeftChildSickLeave - request.TotalDays < 0)
                            {
                                return applyResponse;
                            }
                            break;
                        }
                    case "Compassionate Leave":
                        {
                            if (userLeaveCount.LeftCompassionateLeave - request.TotalDays < 0)
                            {
                                return applyResponse;
                            }
                            break;
                        }
                    case "Maternity Leave":
                        {
                            if (userLeaveCount.LeftMaternityLeave - request.TotalDays < 0)
                            {
                                return applyResponse;
                            }
                            break;
                        }
                    case "Paternity Leave":
                        {
                            if (userLeaveCount.LeftPaternityLeave - request.TotalDays < 0)
                            {
                                return applyResponse;
                            }
                            break;
                        }
                    case "Pilgrimage Leave":
                        {
                            if (userLeaveCount.LeftPilgrimageLeave - request.TotalDays < 0)
                            {
                                return applyResponse;
                            }
                            break;
                        }
                    case "Moving House":
                        {
                            if (userLeaveCount.LeftMovingHouseLeave - request.TotalDays < 0)
                            {
                                return applyResponse;
                            }
                            break;
                        }
                };
            }
            try
            {
               
            
                var leave = new Leave
                {
                    BelongsTo = request.BelongsTo,
                    LeaveType = request.LeaveType,
                    StartDate = request.StartDate,
                    TotalDays = request.TotalDays,
                    IsHalfDay = request.IsHalfDay,
                    FirstHalf = request.FirstHalf,
                    SecondHalf = request.SecondHalf,
                    EndDate = request.EndDate,
                    Supervisor = request.Supervisor,
                    Reason = request.Reason,
                    LeaveStatus = LeaveStatusEnum.Pending,
                };

                _leaveRepository.Add(leave);
                await _leaveRepository.Commit();
            }
            catch (Exception)
            {
                applyResponse.Message ="Something went wrong";

                return applyResponse;
            }

            applyResponse.Response = true;
            applyResponse.Message = "Leave Application Saved.";

            return applyResponse;
        }

        [HttpGet("AppliedStatus")]
        public async Task<ActionResult<List<LeaveViewModel>>> AppliedStatus(string belongsTo)
        {
            var userLeaves = new List<LeaveViewModel>();
            var allLeaves = await _leaveRepository.GetAllAsync(x => x.BelongsTo == belongsTo);

            foreach(Leave leave in allLeaves)
            {
                var supervisor = await _userRepository.GetById(leave.Supervisor);
                var leaveModel = new LeaveViewModel
                {
                    LeaveType = leave.LeaveType,
                    Supervisor = $"{supervisor.FirstName} {supervisor.LastName}",
                    Reason = leave.Reason,
                    StartDate = leave.StartDate,
                    EndDate = leave.EndDate,
                    TotalDays = leave.TotalDays,
                    FirstHalf = leave.FirstHalf,
                    SecondHalf = leave.SecondHalf,
                    LeaveStatus = leave.LeaveStatus
                };

                userLeaves.Add(leaveModel);
            }

            return userLeaves;
        }

        [HttpGet("PendingRequest")]
        public async Task<ActionResult<List<LeaveViewModel>>> PendingRequest(string id, bool supervisor)
        {
            var allRequest = new List<Leave>();
            var requestLeaves = new List<LeaveViewModel>();
            if (supervisor)
            {
               allRequest = await _leaveRepository.GetAllAsync(x => x.Supervisor == id && x.LeaveStatus == LeaveStatusEnum.Pending);
                foreach (Leave leave in allRequest)
                {
                    var user = await _userRepository.GetById(leave.BelongsTo);
                    var pendingModel = new LeaveViewModel
                    {
                        Id = leave.Id,
                        Employee = $"{user.FirstName} {user.LastName}",
                        LeaveType = leave.LeaveType,
                        Reason = leave.Reason,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        TotalDays = leave.TotalDays,
                        FirstHalf = leave.FirstHalf,
                        SecondHalf = leave.SecondHalf,
                        LeaveStatus = leave.LeaveStatus
                    };

                    requestLeaves.Add(pendingModel);
                }

                return requestLeaves;
            }
            else
            {
                allRequest = await _leaveRepository.GetAllAsync(x => x.BelongsTo == id && x.LeaveStatus == LeaveStatusEnum.Pending || x.LeaveStatus == LeaveStatusEnum.Accepted);
                if(allRequest is null)
                {
                    return requestLeaves;
                }
                foreach (Leave leave in allRequest)
                {
                    var leaveSupervisor = await _userRepository.GetById(leave.Supervisor);
                    var pendingModel = new LeaveViewModel
                    {
                        Id = leave.Id,
                        Supervisor = $"{leaveSupervisor.FirstName} {leaveSupervisor.LastName}",
                        LeaveType = leave.LeaveType,
                        Reason = leave.Reason,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        TotalDays = leave.TotalDays,
                        FirstHalf = leave.FirstHalf,
                        SecondHalf = leave.SecondHalf,
                        LeaveStatus = leave.LeaveStatus
                    };

                    requestLeaves.Add(pendingModel);
                }

                return requestLeaves;
            }
           

          
        }

        [HttpPost("ChangeStatus")]
        public async Task<ActionResult<bool>> ChangeStatus(ChangeLeaveStatusRequest request)
        {
            try
            {

                var leave = await _leaveRepository.GetById(request.Id);
                var userLeaveCount = await _leaveCountRepository.GetSingle(x => x.BelongsTo == leave.BelongsTo);
                if (request.Status == 1)
                {
                    switch (leave.LeaveType)
                    {
                        case "Annual Leave":
                            {
                                userLeaveCount.LeftAnnualLeave -= leave.TotalDays;
                                break;
                            }

                        case "Sick Leave":
                            {
                                userLeaveCount.LeftSickLeave -= leave.TotalDays;
                                break;
                            }
                        case "Child Sick Leave":
                            {
                                userLeaveCount.LeftChildSickLeave -= leave.TotalDays;
                                break;
                            }
                        case "Compassionate Leave":
                            {
                                userLeaveCount.LeftCompassionateLeave -= leave.TotalDays;
                                break;
                            }
                        case "Maternity Leave":
                            {
                                userLeaveCount.LeftMaternityLeave -= leave.TotalDays;
                                break;
                            }
                        case "Paternity Leave":
                            {
                                userLeaveCount.LeftPaternityLeave -= leave.TotalDays;
                                break;
                            }
                        case "Pilgrimage Leave":
                            {
                                userLeaveCount.LeftPaternityLeave -= leave.TotalDays;
                                break;
                            }
                        case "Moving House":
                            {
                                userLeaveCount.LeftMovingHouseLeave -= leave.TotalDays;
                                break;
                            }
                    };
                }
                leave.LeaveStatus = (LeaveStatusEnum)(request.Status);
                _leaveRepository.Update(leave);
                _leaveCountRepository.Update(userLeaveCount);
                await _leaveRepository.Commit();
                await _leaveCountRepository.Commit();

            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        [HttpGet("Remove")]
        public async Task<ActionResult<bool>> Remove([FromQuery]string id)
        {
            var leave = await _leaveRepository.GetById(id);
            var userLeaveCount = await _leaveCountRepository.GetSingle(x => x.BelongsTo == leave.BelongsTo);

            if (leave.LeaveStatus == LeaveStatusEnum.Accepted)
            {
                switch (leave.LeaveType)
                {
                    case "Annual Leave":
                        {
                            userLeaveCount.LeftAnnualLeave += leave.TotalDays;
                            break;
                        }

                    case "Sick Leave":
                        {
                            userLeaveCount.LeftSickLeave += leave.TotalDays;
                            break;
                        }
                    case "Child Sick Leave":
                        {
                            userLeaveCount.LeftChildSickLeave += leave.TotalDays;
                            break;
                        }
                    case "Compassionate Leave":
                        {
                            userLeaveCount.LeftCompassionateLeave += leave.TotalDays;
                            break;
                        }
                    case "Maternity Leave":
                        {
                            userLeaveCount.LeftMaternityLeave += leave.TotalDays;
                            break;
                        }
                    case "Paternity Leave":
                        {
                            userLeaveCount.LeftPaternityLeave += leave.TotalDays;
                            break;
                        }
                    case "Pilgrimage Leave":
                        {
                            userLeaveCount.LeftPaternityLeave += leave.TotalDays;
                            break;
                        }
                    case "Moving House":
                        {
                            userLeaveCount.LeftMovingHouseLeave += leave.TotalDays;
                            break;
                        }
                };
            }

            try
            {
                _leaveRepository.Remove(id);
                _leaveCountRepository.Update(userLeaveCount);
                await _leaveRepository.Commit();
                await _leaveCountRepository.Commit();

            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }
    }

}
