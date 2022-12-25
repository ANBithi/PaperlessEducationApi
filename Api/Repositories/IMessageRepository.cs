using Api.Repositories;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
    }
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IDbContext context) : base(context)
        {

        }
    }
}

