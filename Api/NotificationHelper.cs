using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;
using Api.Requests.NotificationRequests;
using System;

namespace Api
{
   
    public class NotificationHelper
    {
        private HttpClient client = new HttpClient();
        const string notificationUrl = "http://localhost:3001/onNotification";
        public async void Notify(NotificationRequest request)
        {
            try
            {
                var camelSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                var req = JsonConvert.SerializeObject(request, camelSettings);
                var data = new StringContent(req, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(notificationUrl, data);
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error in notification - {e.Message}");
                
            }
        }

    }
}
