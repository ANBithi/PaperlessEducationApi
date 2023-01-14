using Api.Models.Post;

namespace Api.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
    }
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbContext context) : base(context)
        {

        }
    }
}
