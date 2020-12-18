using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lazcat.Blog.Infrastructure.Exceptions
{
    public class HttpException
    {
        private HttpResponseMessage _httpResponse;

        public HttpException()
        {
        }

        public async Task<HttpResponseMessage> Create(HttpStatusCode status, string content)
        {
            var httpResponseMessage = new HttpResponseMessage(status) {Content = new StringContent(content)};
            return httpResponseMessage;
        }
    }
}