using Api.CustomValues;
using Api.Enums;
using System.Collections.Generic;

namespace Api.Requests
{
    public class AddQuestionRequest
    {
        public string Content { get; set; }
        public string ExamId { get; set; }
        public float Mark { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public List<QuestionOption> Options { get; set; }
    }

    public class UpdateQuestionRequest
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public float Mark { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public List<QuestionOption> Options { get; set; }
    }
}
