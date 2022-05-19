using BIT.Api.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BIT.Api.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not ApiException apiException) return;
        
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