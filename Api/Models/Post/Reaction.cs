namespace Api.Models.Post
{
    public class Reaction : AbstractDbEntity
    {        
        public string ParentId { get; set; }
        public string IconId { get; set; }
    }
}
