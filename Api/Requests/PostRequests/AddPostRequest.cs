using Api.ViewModels;
using System.Collections.Generic;

namespace Api.Requests.PostRequests
{
    public class AddPostRequest
    {
        public string BelongsTo { get; set; }
        public int CreatorType { get; set; }
        public string PostDescription { get; set; }
        public List<AttachmentVM> Attachments { get; set; }
    }
}
