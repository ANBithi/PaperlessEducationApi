using Api.Commons;
using Api.Enums;
using Api.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Api.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserTypeValidatorAttribute : Attribute, IAsyncActionFilter
    {
        private readonly UserTypeEnum _userType;
        public UserTypeValidatorAttribute(UserTypeEnum userType)
        {
            _userType = userType;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var _requestUserService = context.HttpContext.RequestServices.GetRequiredService<IRequestUserService>();

                var user = await _requestUserService.GetUser();


                var isValidated = new UserTypeValidator(user, _userType).Validate();
                if (isValidated)
                {
                    await next();
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
               
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }

}
