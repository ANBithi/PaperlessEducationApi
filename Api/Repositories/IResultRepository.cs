using Api.Repositories;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IResultRepository :IRepository<Result>
    {
    }
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {
        public ResultRepository(IDbContext context) : base(context)
        {

        }
    }
}
