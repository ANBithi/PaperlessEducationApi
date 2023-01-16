using Api.Models.UserInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IUserInteractionRepository : IRepository<UserInteraction>
    {
    }
    public class UserInteractionRepositoy : BaseRepository<UserInteraction>, IUserInteractionRepository
    {
        public UserInteractionRepositoy(IDbContext context) : base(context)
        {

        }
    }
}
