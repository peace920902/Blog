using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Dtos.Messages;
using Lazcat.Blog.Models.Web;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

namespace Lazcat.Blog.Web.Provider.Messages
{
    public class MessageProvider:ProviderBase, IMessageProvider
    {
        private readonly HttpClient _http;
        public const string Message = "message";
        
        public MessageProvider(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient(Define.DefaultHttpClient);
        }

        public async Task<ResponseMessage<IEnumerable<MessageDto>>> GetMessages(int articleId)
        {
            return await SendRequest($"{Message}/{articleId}", _http.GetFromJsonAsync<IEnumerable<MessageDto>>);
        }

        public async Task<ResponseMessage<MessageDto>> CreateMessage(CreateUpdateMessageInput input)
        {
            return await SendNeedDeserializedRequest<MessageDto, CreateUpdateMessageInput>(Message, _http.PostAsJsonAsync, input);
        }

        public async Task<ResponseMessage<MessageDto>> UpdateMessage(CreateUpdateMessageInput input)
        {
            return await SendNeedDeserializedRequest<MessageDto, CreateUpdateMessageInput>(Message, _http.PutAsJsonAsync, input);
        }

        public async Task<ResponseMessage<MessageDto>> DeleteMessage(Guid id)
        {
            return await SendNeedDeserializedRequest<MessageDto>($"{Message}/{id}", _http.DeleteAsync);
        }
    }
}