using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IEmployeeInfoRepository : IRepository<EmployeeInfo>
    {
    }

    public class EmployeeInfoRepository : BaseRepository<EmployeeInfo>, IEmployeeInfoRepository
    {
        public EmployeeInfoRepository(IDbContext context) : base(context)
        {

        }
    }
}
