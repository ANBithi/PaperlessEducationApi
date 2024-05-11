using Api.Models;

namespace Api.Repositories
{
    public interface IInstituteRepository : IRepository<Institute>
    {

    }


    public class InstituteRepository : BaseRepository<Institute>, IInstituteRepository
    {
        public InstituteRepository(IDbContext context) : base(context)
        {

        }
    }
}
