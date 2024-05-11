using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context)
        {
            
        }

    }
}
