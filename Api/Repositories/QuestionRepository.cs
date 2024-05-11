using Api.Models;

namespace Api.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IDbContext context) : base(context)
        {
        }
    }
}
