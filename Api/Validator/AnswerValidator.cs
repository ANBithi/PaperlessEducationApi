using Api.Repositories;
using System;

namespace Api.Validators
{
    public class AnswerValidator : IValidator
    {
        private readonly IExamRepository _examRepository;

        public AnswerValidator(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public void AddAnswerValidation(string questionId, string examId)
        {
           

        }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
