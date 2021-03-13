using System;

namespace Lazcat.Blog.Models.Infrastructure
{
    public class HttpException
    {
        public HttpException()
        {
            DateTime = DateTime.Now;
        }

        public HttpException(string content)
        {
            Content = content;
            DateTime = DateTime.Now;
        }

        public string Content { get; set; }
        public DateTime DateTime { get; set; }
    }
}