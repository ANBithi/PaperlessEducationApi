using Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class LeaveViewModel
    {
        public string Id { get; set; }
        public string Employee { get; set; }
        public string LeaveType { get; set; }
        public string Supervisor { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
        public bool FirstHalf { get; set; }
        public bool SecondHalf { get; set; }
        public LeaveStatusEnum LeaveStatus { get; set; }
    }

    public class AddLeaveRequest
    {
        public string BelongsTo { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalDays { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsHalfDay { get; set; }
        public bool FirstHalf { get; set; }
        public bool SecondHalf { get; set; }
        public string Supervisor { get; set; }
        public string Reason { get; set; }
    }


    public class GetLeaveStatusRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BelongsTo { get; set; }
    }
    public class ChangeLeaveStatusRequest
    {
        public string Id { get; set; }
        public int Status { get; set; }
    }
}
