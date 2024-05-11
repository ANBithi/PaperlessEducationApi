using Api.Models;

namespace Api.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
    }

    public class DepertmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepertmentRepository(IDbContext context) : base(context)
        {

        }
    }
}
