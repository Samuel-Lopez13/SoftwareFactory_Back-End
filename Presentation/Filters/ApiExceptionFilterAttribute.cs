using Core.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<ApiExceptionFilterAttribute> _logger;
    
    public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case BadRequestException badRequestException:
                HandleBadRequestException(context, badRequestException);
                _logger.LogDebug(context.Exception, "{message} en {@Result}", context.Exception.Message,
                    context.Result);
                break;
        }
    }
    
    private void HandleBadRequestException(ExceptionContext context, BadRequestException exception)
    {
        var details = new ProblemDetails()
        {
            Title = "Los datos ingresados son incorrectos",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Detail = exception.Message
        };
        
        context.Result = new BadRequestObjectResult(details);
        
        context.ExceptionHandled = true;
    }
}