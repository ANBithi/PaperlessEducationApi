namespace Api.Requests.UserRequests
{
    public class ChangePasswordRequest
    {
        public int Otp { get; set; }
        public string NewPassword { get; set; }
    }
}
