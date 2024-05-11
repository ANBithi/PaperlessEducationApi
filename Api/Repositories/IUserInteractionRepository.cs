using Api.Models;

namespace Api.Repositories
{
    public interface IUserInteractionRepository : IRepository<UserInteraction>
    {
    }
    public class UserInteractionRepositoy : BaseRepository<UserInteraction>, IUserInteractionRepository
    {
        public UserInteractionRepositoy(IDbContext context) : base(context)
        {

        }
    }
}
