using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.UserRequests
{
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
}
