using Api.Models;

namespace Api.Requests
{
    public class AddActivityRequest
    {
        public string UserId { get; set; }
        public ActiveStatusEnum Status { get; set; }
        public ActiveStatuForTypeEnum ActivityFor { get; set; }
        public string ActivityForId { get; set; }
    }
}
