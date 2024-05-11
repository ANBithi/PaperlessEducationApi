using Api.Models;

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

