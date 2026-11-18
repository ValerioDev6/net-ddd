using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquitectura.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;


    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An exception occurred");
            var exceptionDetails = GetExceptionDetails(e);

            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Type = exceptionDetails.Type,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Detail,
            };

            if (exceptionDetails.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }
            
            context.Response.StatusCode = exceptionDetails.Status;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        
        
    }
    
    private static ExceptionDetails GetExceptionDetails(Exception ex)
    {
        return ex switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validation Error",
                "Han ocurrido uno o mas erroes de validaicon",
                validationException.Errors
                ),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "Server error",
                "Error de servidor",
                "Un inespedaro error a ocurrido en la app", null)
            
        };
    }

    internal record ExceptionDetails
    (
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);


    
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
