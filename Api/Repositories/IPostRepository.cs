using Api.Repositories;
using Api.Models;

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
