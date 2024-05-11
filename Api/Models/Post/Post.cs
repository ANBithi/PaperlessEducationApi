using Api.CustomValues;
using System.Collections.Generic;

namespace Api.Models.Post
{
    public class Post : AbstractDbEntity
    {
        public string BelongsTo { get; set; }
        public int CreatorType { get; set; }
        public string PostDescription { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
