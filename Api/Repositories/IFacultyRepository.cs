using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
