using Api.Enums;
using System.Collections.Generic;

namespace Api.Requests
{
    public class AddCourseRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string DepartmentId { get; set; }
        public LessonType LessonType { get; set; }
        public string CourseDetails { get; set; }
        public CourseTypeEnum Type { get; set; }
        public List<string> Prerequisites { get; set; }
    }
}
