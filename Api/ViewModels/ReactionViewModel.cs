using System;

namespace Api.ViewModels
{
    public class ReactionViewModel
    {
        public string Id { get; set; }
        public string IconId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }

    }

    public class CommentViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ParentId { get; set; }
    }
}
