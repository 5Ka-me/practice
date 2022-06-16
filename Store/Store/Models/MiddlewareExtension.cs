namespace Store.Models
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseCustomHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    } 
}
