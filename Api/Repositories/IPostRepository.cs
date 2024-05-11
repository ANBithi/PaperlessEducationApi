using Api.Models.Post;

namespace Api.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {

    }


    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IDbContext context) : base(context)
        {

        }
    }
}
