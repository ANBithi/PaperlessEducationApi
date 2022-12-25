using Api.Models;
using Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
