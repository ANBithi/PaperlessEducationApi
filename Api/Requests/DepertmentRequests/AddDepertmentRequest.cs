using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests
{
    public class AddDepertmentRequest
    {
        public string Name { get; set; }
        public string CourseDistribution { get; set; }
        public string Abbreviation { get; set; }
        public int DepartmentCode { get; set; }

    }
}
