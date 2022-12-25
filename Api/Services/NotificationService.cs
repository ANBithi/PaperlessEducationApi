using Api.Models;
using Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;


        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public void CreateNotification(NotifyRequest request)
        {
            var notification = new Notification
            {
                Type = request.Type,
                DataId = request.DataId
            };
            _notificationRepository.Add(notification);
        }
    }
}
