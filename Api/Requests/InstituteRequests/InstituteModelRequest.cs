namespace Api.Requests.InstituteRequests
{
    public class InstituteModelRequest
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string Domain { get; set; }
        public int EstablishedYear { get; set; }
        public string About { get; set; }
        public int SemesterDuration { get; set; }
        public string Contact { get; set; }
    }
}
