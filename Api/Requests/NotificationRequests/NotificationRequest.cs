namespace Api.Requests.NotificationRequests
{
    public class NotificationRequest
    {
        public string Type { get; set; }
        public string Target { get; set; }
        public string DataId { get; set; }
        public string CreatedById { get; set; }
    }
}
