using Api.Exceptions;
using Api.Validators;

namespace Api.Validator
{
    public class NameValidator : IValidator
    {
        private readonly string _firstName;
        private readonly string _lastName;
        public NameValidator(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(_firstName))
            {
                if (string.IsNullOrEmpty(_lastName))
                {
                    throw new InvalidRequestException("First name and last name are empty.");
                }
                else
                {
                    throw new InvalidRequestException("First name is empty.");
                }
            }
            return true;
        }
    }
}
