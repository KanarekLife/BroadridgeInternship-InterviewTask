using BIT.Api.Infrastructure.WordTimeApiTimeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BIT.Api.Filters;

public class WorldTimeApiInvalidTimezoneFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not WorldTimeApiInvalidTimezoneException invalidTimezoneException) return;
        
        var problem = new ProblemDetails()
        {
            Title = invalidTimezoneException.Title,
            Detail = invalidTimezoneException.Details,
            Status = invalidTimezoneException.StatusCode,
            Type = context.Exception.GetType().Name
        };
        problem.Extensions.Add("CurrentTimezone", invalidTimezoneException.CurrentTimezone);
        problem.Extensions.Add("ValidTimezones", invalidTimezoneException.ValidTimezones);
        context.Result = new ObjectResult(problem);
        context.ExceptionHandled = true;
    }
}