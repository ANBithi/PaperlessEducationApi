using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.DepertmentRequests
{
    public class AddDepertmentRequest
    {
        public string Name { get; set; }
        public string CourseDistribution { get; set; }
        public int TotalCredits { get; set; }
        public int MinimumCreditPerSem { get; set; }

    }
}
