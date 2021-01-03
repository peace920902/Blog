using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web.Http;
using Lazcat.Blog.Models.Infrastructure;

namespace Lazcat.Blog.Infrastructure
{
    public static class ExceptionBuilder
    {
        public static HttpResponseException Build(HttpStatusCode statusCode, HttpException ex)
        {
            return new(new HttpResponseMessage(statusCode)
            {
                ReasonPhrase = JsonSerializer.Serialize(ex)
            });
        }
    }
}