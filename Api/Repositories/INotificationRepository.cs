using Api.Models;

namespace Api.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
    }

    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(IDbContext context) : base(context)
        {

        }
    }
}
