using Api.IServices;
using Api.Models;
using Api.Repositories;
using Api.Requests.NotificationRequests;
using System;

namespace Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;


        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public void CreateNotification(NotificationRequest request)
        {
            var notification = new Notification
            {
                Type = request.Type,
                DataId = request.DataId
            };
            notification.CreatedAt = DateTime.Now;
            _notificationRepository.Add(notification);

            var a = new NotificationRequest
            {
                Target = "newNotification",
            };
            var notifcationHelper = new NotificationHelper();
            notifcationHelper.Notify(a);

        }
    }
}
