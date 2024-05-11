using Api.Enums;
using System;

namespace Api.ViewModels
{
    public class ExamMetadataViewModel
    {

        public string Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string SectionNumber { get; set; }
        public string SectionId { get; set; }
        public string ExamTitle { get; set; }
        public string AnswerType { get; set; }
        public CountPercentileEnum CountPercentile { get; set; }
        public string Duration { get; set; }
        public DateTime StartTime { get; set; }
        public int TotalMarks { get; set; }
        public ExamTypeEnum ExamType { get; set; }
    }
}