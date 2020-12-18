using Lazcat.Blog.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace Lazcat.Blog.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseCustomerExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}