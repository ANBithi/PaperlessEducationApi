namespace Api.Requests.PostRequests
{
    public class AddCommentRequest
    {
        public string Content { get; set; }
        public string ParentId { get; set; }
    }
}
