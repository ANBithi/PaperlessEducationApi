using Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Requests.Exam
{
    public class AddExamMetadataRequest
    {
        public string CourseName { get; set; }
        public string SectionNumber { get; set; }
        public string SectionId { get; set; }
        public string ExamTitle { get; set; }
        public string AnswerType { get; set; }
        public CountPercentileEnum CountPercentile { get; set; }
        public string Duration { get; set; }
        public string StartTime { get; set; }
        public int TotalMarks { get; set; }
        public ExamTypeEnum ExamType { get; set; }
    }
}
