using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private static Dictionary<Type, HttpStatusCode> SupportedExceptionsWithStatusCode = new()
    {
        { typeof(ArgumentException), HttpStatusCode.BadRequest },
        { typeof(ArgumentNullException), HttpStatusCode.BadRequest },
        { typeof(FieldInUseException), HttpStatusCode.BadRequest },
        { typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized },
        { typeof(NotFoundException), HttpStatusCode.NotFound },
        { default, HttpStatusCode.InternalServerError }
    };

    public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
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
        context.Response.StatusCode = (int)SupportedExceptionsWithStatusCode.GetValueOrDefault(exception.GetType());
        return context.Response.WriteAsync(JsonSerializer.Serialize<ErrorDTO>(new(exception.Message)));
    }
}