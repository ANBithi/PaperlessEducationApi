using Api.Models;

namespace Api.Repositories
{
    public interface IFacultyRepository : IRepository<Faculty>
    {

    }


    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(IDbContext context) : base(context)
        {

        }
    }
}
