using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Api.Services;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public class NotifyRequest {
        public string Type { get; set; }
        public string Target { get; set; }
        public string DataId { get; set; }
    }
    public class NotificationHelper
    {
        private HttpClient client = new HttpClient();
        const string notificationUrl = "http://localhost:3001/onNotification";
        public async void Notify(NotifyRequest request)
        {
            var camelSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var req = JsonConvert.SerializeObject(request, camelSettings);
            var data = new StringContent(req, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(notificationUrl, data);
        }

    }
}
