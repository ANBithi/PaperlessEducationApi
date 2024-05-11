using Api.Enums;

namespace Api.Models
{
    public class AuthData
    {
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserInitials { get; set; }
        public string Email { get; set; }
        public string TenantId { get; set; }
        public string RoleId { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
