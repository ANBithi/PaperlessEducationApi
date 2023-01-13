using Api.ViewModels;
namespace Api.Responses.UserResponses
{
    public class LoginStatus
    {
        public UserViewModel User { get; set; }
        public bool IsAuthorized { get; set; }
        public string Message { get; set; }
    }
}
