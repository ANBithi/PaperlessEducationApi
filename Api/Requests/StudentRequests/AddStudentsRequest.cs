namespace Api.Requests.StudentRequests
{
    public class AddStudentsRequest
    {
        public string Students { get; set; }
        public string Department { get; set; }
        public string AdvisorId { get; set; }
        public string Batch { get; set; }
    }
}
