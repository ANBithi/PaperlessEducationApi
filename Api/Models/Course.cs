using Api.Enums;
using System.Collections.Generic;

namespace Api.Models
{
    public class Course : AbstractDbEntity
    {
        public string DepartmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CourseDetails { get; set; }
        public CourseTypeEnum Type { get; set; }
        public LessonType LessonType { get; set; }
        public List<string> Prerequisites { get; set; }
        public int Credits { get; set; }
        public string CourseCover { get; set; }
    }
}
