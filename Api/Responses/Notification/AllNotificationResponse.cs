using Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Responses.Notification
{
    public class AllNotificationResponse
    {
        public List<NotificationViewModel> OldNotifications { get; set; }
        public List<NotificationViewModel> NewNotifications { get; set; }
        public int UnreadCount { get; set; }
    }
}
