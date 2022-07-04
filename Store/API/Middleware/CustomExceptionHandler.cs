using System.Net;
using System.Text.Json;

namespace API.Models
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case ArgumentNullException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case OperationCanceledException:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var result = JsonSerializer.Serialize(new ExceptionModel { StatusCode = statusCode, Message = exception.Message });

            return context.Response.WriteAsync(result);
        }
    }
}
