using Api.ViewModels;
using System.Collections.Generic;

namespace Api.Responses.MessageResponses
{
    public class MessageResponse
    {
        public bool Response { get; set; }
        public List<MessageViewModel> Data { get; set; }
    }
}
