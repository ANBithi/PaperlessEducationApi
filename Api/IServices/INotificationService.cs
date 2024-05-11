using Api.Requests.NotificationRequests;

namespace Api.IServices
{
    public interface INotificationService
    {
        void CreateNotification(NotificationRequest request);
    }
}
