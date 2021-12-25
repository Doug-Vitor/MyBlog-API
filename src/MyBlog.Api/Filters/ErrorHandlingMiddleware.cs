using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private static Dictionary<Type, int> SupportedExceptionsWithStatusCode = new()
    {
        { typeof(ArgumentException), (int)HttpStatusCode.BadRequest },
        { typeof(ArgumentNullException), (int)HttpStatusCode.BadRequest },
        { typeof(FieldInUseException), (int)HttpStatusCode.BadRequest },
        { typeof(UnauthorizedAccessException), (int)HttpStatusCode.Unauthorized },
        { typeof(NotFoundException), (int)HttpStatusCode.NotFound },
    };

    public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            await HandleExceptionAsync(context, error);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = SupportedExceptionsWithStatusCode.GetValueOrDefault(exception.GetType());
        return context.Response.WriteAsync(JsonSerializer.Serialize<ErrorDTO>(new(exception.Message)));
    }
}