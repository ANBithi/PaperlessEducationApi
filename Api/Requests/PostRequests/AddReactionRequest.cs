using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.PostRequests
{
    public class AddReactionRequest
    {
        public string IconId { get; set; }
        public string ParentId { get; set; }
        public string CreatedBy { get; set; }
    }
}
