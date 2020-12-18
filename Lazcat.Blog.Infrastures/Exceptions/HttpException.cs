
using System;
using System.Net;

namespace Lazcat.Blog.Infrastructure.Exceptions
{
    public class HttpException 
    {
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public HttpException()
        {
            DateTime = DateTime.Now;
        }
    }
}