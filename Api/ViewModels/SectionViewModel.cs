using System.Collections.Generic;

namespace Api.ViewModels
{
    public class SectionViewModel
    {
        public string SectionId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string SectionNumber { get; set; }
        public List<StudentViewModel> Students { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CourseDescription { get; set; }
        public string CourseCover { get; set; }
    }
}
