using Api.CustomValues;
using System;
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class PostViewerModel
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string PostDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
