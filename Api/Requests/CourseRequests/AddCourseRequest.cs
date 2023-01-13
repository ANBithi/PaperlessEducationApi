using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.CourseRequests
{
    public class AddCourseRequest
    {
        public string BelongsTo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Department { get; set; }
        public string CourseDetails { get; set; }
        public int Type { get; set; }
        public List<string> PreRequisities { get; set; }
        public int Credits { get; set; }
    }
}
