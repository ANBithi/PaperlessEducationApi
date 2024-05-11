using Api.ViewModels;
using System.Collections.Generic;

namespace Api.Responses.Notification
{
    public class AllNotificationResponse
    {
        public List<NotificationViewModel> OldNotifications { get; set; }
        public List<NotificationViewModel> NewNotifications { get; set; }
        public int UnreadCount { get; set; }
    }
}
