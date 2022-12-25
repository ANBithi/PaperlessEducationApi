using Api.Models;

namespace Api.Repositories
{
    public interface ILeaveCountRepository : IRepository<LeaveCount>
    {

    }


    public class LeaveCountRepository : BaseRepository<LeaveCount>, ILeaveCountRepository
    {
        public LeaveCountRepository(IDbContext context) : base(context)
        {

        }
    }
}
