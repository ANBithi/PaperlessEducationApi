using Api.Models;

namespace Api.Repositories
{
    public interface IExamRepository : IRepository<Exam>
    {
    }
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(IDbContext context) : base(context)
        {
        }
    }
}
