using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.NotificationRequests
{
    public class NotificationRequest
    {
        public string Type { get; set; }
        public string Target { get; set; }
        public string DataId { get; set; }
    }
}
