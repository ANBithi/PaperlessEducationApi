using Api.Models;

namespace Api.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
    }

    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(IDbContext context) : base(context)
        {

        }
    }
}
