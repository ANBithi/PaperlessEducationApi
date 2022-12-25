using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
