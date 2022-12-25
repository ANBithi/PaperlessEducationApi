using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context)
        {
            
        }

    }
}
