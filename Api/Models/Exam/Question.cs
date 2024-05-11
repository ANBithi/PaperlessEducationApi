using Api.CustomValues;
using Api.Enums;
using System.Collections.Generic;

namespace Api.Models
{

    public class Question: AbstractDbEntity
    {
        public QuestionTypeEnum  QuestionType { get; set; }
        public string Content { get; set; }
        public double Mark { get; set; }
        public string ExamId { get; set; }
        public List<QuestionOption> Options { get; set; }
    }
 
}
