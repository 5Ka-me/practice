using System.Net;
using System.Text.Json;

namespace Store.Models
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExeptionAsync(context, exception);
            }
        }

        private Task HandleExeptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string result;

            switch (exception)
            {
                case ArgumentNullException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            result = JsonSerializer.Serialize(new ExceptionModel {StatusCode = statusCode, Message = exception.Message });

            return context.Response.WriteAsync(result);
        }
    }
}
