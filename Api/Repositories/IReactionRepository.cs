using Api.Models.Post;

namespace Api.Repositories
{
    public interface IReactionRepository : IRepository<Reaction>
    {
    }
    public class ReactionRepository : BaseRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(IDbContext context) : base(context)
        {

        }
    }
}
