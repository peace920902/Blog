using System;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lazcat.Blog.Infrastructure.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleException(httpContext, e);
            }
        }

        private Task HandleException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            var message = exception.Message;
            _logger.LogError(exception, $"Ex massage: {message}, StackTrace: {exception.StackTrace}", exception);

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(new HttpException { Content = "Somethings error."}));
        }
    }
}