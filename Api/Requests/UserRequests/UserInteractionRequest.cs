using Api.Models.UserInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.UserRequests
{
    public class UserInteractionRequest
    {
        public string CreatedById { get; set; }
        public InteractionType Type { get; set; }
    }
}
