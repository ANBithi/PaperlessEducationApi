using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.StudentRequests
{
    public class AddStudentsRequest
    {
        public string Students { get; set; }
        public string Department { get; set; }
        public string AdvisorId { get; set; }
        public string Batch { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }

    }
}
