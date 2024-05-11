using Api.Models;

namespace Api.Repositories
{
    public class UserActivityRepositoy : BaseRepository<UserActivity>, IUserActivityRepository
    {
        public UserActivityRepositoy(IDbContext context) : base(context)
        {

        }
    }
}
