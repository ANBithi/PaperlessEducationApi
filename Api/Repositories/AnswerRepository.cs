using Api.Models;

namespace Api.Repositories
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(IDbContext context) : base(context)
        {
        }
    }
}
