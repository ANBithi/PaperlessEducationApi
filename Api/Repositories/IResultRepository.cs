using Api.Models;

namespace Api.Repositories
{
    public interface IResultRepository :IRepository<Result>
    {
    }
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {
        public ResultRepository(IDbContext context) : base(context)
        {

        }
    }
}
