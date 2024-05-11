using Api.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Api.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LiveUpdateServiceValidateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var _serviceSecrateValidation = context.HttpContext.RequestServices.GetRequiredService<IServiceSecretValidationService>();

                var isValidated = await _serviceSecrateValidation.ValidateService();
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
                Console.WriteLine(ex.Message);
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }

}
