namespace Api.Models.Post
{
    public class Comment : AbstractDbEntity
    {
        public string ParentId { get; set; }
        public string Content { get; set; }
    }
}
