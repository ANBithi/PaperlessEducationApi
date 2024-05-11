using Api.Enums;
using Api.Exceptions;
using Api.Validators;
using System;

namespace Api.Validator
{
    public class RoleValidator : IValidator
    {
        private readonly string _role;

        public RoleValidator( string role)
        {
            _role = role;
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(_role))
            {
                    throw new InvalidRequestException("User type is empty");
            }


            if (!Enum.IsDefined(typeof(UserTypeEnum), _role))
            {
                throw new InvalidRequestException("User type does not exist");

            }

            return true;
        }
    }

}
