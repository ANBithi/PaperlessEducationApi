
using Api.Enums;

namespace Api.ViewModels
{
    public class AnswerViewModel
    {
        public string Content { get; set; }
        public double ObtainedMark { get; set; }
        public EvaluationStatus EvaluationStatus { get; set; }
    }

}
