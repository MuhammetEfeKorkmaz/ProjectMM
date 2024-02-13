using Microsoft.AspNetCore.Builder;

namespace FullSharedCore.Extensions.PacketCustomException
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionExtensionMiddleware>();
        }
    }
}
