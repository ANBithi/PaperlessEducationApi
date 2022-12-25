using Api.Repositories;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
