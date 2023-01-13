using System;

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
}
