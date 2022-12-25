using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
