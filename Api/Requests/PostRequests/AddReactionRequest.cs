namespace Api.Requests.PostRequests
{
    public class AddReactionRequest
    {
        public string IconId { get; set; }
        public string ParentId { get; set; }
        public string CreatedBy { get; set; }
    }
}
