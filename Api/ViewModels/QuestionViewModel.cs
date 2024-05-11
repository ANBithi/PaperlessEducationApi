
using Api.CustomValues;
using Api.Enums;
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionTypeEnum QuestionType { get; set; }
        public string Content { get; set; }
        public double Mark { get; set; }
        public string ExamId { get; set; }
        public string Id { get; set; }
        public AnswerViewModel Answer { get; set; }
        public List<QuestionOption> Options { get; set; }
    }
}
