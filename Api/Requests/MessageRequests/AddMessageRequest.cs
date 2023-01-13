using Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.MessageRequests
{
    public class AddMessageRequest
    {
        public string BelongsTo { get; set; }
        public string CreatedBy { get; set; }
        public int CreatorType { get; set; }
        public string MessageBody { get; set; }
        public List<AttachmentVM> Attachments { get; set; }
    }
}
