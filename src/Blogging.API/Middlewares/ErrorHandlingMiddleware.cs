

using Azure;
using Blogging.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Blogging.API.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch(NotFoundException notFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);
            _logger.LogWarning(notFound.Message);

        }
        catch (ForbidException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access forbidden");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            context.Response.StatusCode = 500;
            //await context.Response.WriteAsync(ex.Message);
            await context.Response.WriteAsJsonAsync(ex.Message);
        }
    }
}
