using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IResignRepository : IRepository<Resign>
    {

    }


    public class ResignRepository : BaseRepository<Resign>, IResignRepository
    {
        public ResignRepository(IDbContext context) : base(context)
        {

        }
    }
}
