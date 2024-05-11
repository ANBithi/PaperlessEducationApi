using Api.CustomValues;
using Api.Enums;
using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class Exam: AbstractDbEntity
    {
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
        public List<QuestionOption> Questions { get; set; }
    }


   
}
