using Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class StudenViewer
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public ResultViewModel Result { get; set; }
    }
    public class SectionViewModel
    {
        public string SectionId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string SectionNumber { get; set; }
        public List<StudenViewer> Students { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string CourseDescription { get; set; }
    }
}
