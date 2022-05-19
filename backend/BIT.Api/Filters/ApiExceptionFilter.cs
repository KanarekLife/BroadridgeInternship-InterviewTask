using BIT.Api.Core;
using BIT.Api.Infrastructure.WordTimeApiTimeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BIT.Api.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not ApiException apiException) return;

        if (context.Exception is WorldTimeApiInvalidTimezoneException invalidTimezoneException)
        {
            var problem = new ProblemDetails()
            {
                Title = apiException.Title,
                Detail = apiException.Details,
                Status = apiException.StatusCode,
                Type = context.Exception.GetType().Name
            };
            problem.Extensions.Add("CurrentTimezone", invalidTimezoneException.CurrentTimezone);
            problem.Extensions.Add("ValidTimezones", invalidTimezoneException.ValidTimezones);
            context.Result = new ObjectResult(problem);
            context.ExceptionHandled = true;
            return;
        }
        
        context.Result = new ObjectResult(new ProblemDetails
        {
            Title = apiException.Title,
            Detail = apiException.Details,
            Status = apiException.StatusCode,
            Type = context.Exception.GetType().Name
        });
        context.ExceptionHandled = true;
    }
}