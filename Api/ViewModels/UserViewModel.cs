using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public class CreateUserRequest
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public int UserType { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }

    }
    public class AddStudentsRequest
    {
        public string Students { get; set; }
        public string Department { get; set; }
        public string AdvisorId { get; set; }
        public string Batch { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }

    }


}
