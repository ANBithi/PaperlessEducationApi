using Api.Models;

namespace Api.Repositories
{
    public interface ISectionRepository : IRepository<Section>
    {
    }


    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
       public SectionRepository(IDbContext context) : base(context)
        {

        }
    }
}
