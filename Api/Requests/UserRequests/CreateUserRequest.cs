namespace Api.Requests.UserRequests
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string UserType { get; set; }


    }
}
