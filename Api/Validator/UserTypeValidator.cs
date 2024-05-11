using Api.Enums;
using Api.Models;
using Api.Validators;

namespace Api.Validator
{
    public class UserTypeValidator : IValidator
    {
        private readonly User _user;
        private readonly UserTypeEnum _targetUserType;
        public UserTypeValidator(User user, UserTypeEnum targetUserType)
        {
            _user = user;
            _targetUserType = targetUserType;
        }

        public bool Validate()
        {
            if(_user.UserType == UserTypeEnum.Root)
            {
                return true;
            }
            return (_user.UserType == _targetUserType);
        }
    }
}
