using System.Net;

namespace Lazcat.Blog.Models.Infrastructure
{
    public class ErrorMessage
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Content { get; set; }
    }
}