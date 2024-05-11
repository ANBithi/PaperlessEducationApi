using Api.Enums;

namespace Api.Models
{

    public class Answer: AbstractDbEntity
    {

        public string QuestionId { get; set; }
        public string  ExamId { get; set; }
        public string AnswerType { get; set; }
        public string Content { get; set; }
        public double ObtainedMark { get; set; }
        public EvaluationStatus EvaluationStatus { get; set; }
    }
}
