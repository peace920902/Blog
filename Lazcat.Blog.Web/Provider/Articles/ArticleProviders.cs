using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider.Articles
{
    public class ArticleProviders : ProviderBase, IArticleProvider
    {
        private readonly HttpClient _http;
        public const string Article = "Article";
        public ArticleProviders(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient(Setting.DefaultHttpClient);
        }
        public async Task<ResponseMessage<IEnumerable<ArticleDto>>> GetArticles(bool isGetContent=false)
        {
            return await SendRequest($"{Article}/all?isGetContent={isGetContent}", _http.GetFromJsonAsync<IEnumerable<ArticleDto>>);
        }

        public async Task<ResponseMessage<ArticleDto>> GetArticle(int id)
        {
            return await SendRequest($"{Article}/{id}", _http.GetFromJsonAsync<ArticleDto>);
        }

        public async Task<ResponseMessage<ArticleDto>> CreateArticle(CreateUpdateArticleInput input)
        {
            return await SendNeedDeserializedRequest<ArticleDto, CreateUpdateArticleInput>(Article, _http.PostAsJsonAsync, input);

        }

        public async Task<ResponseMessage<ArticleDto>> UpdateArticle(CreateUpdateArticleInput input)
        {
            return await SendNeedDeserializedRequest<ArticleDto, CreateUpdateArticleInput>($"{Article}/content", _http.PutAsJsonAsync, input);
        }

        public async Task<ResponseMessage<bool>> DeleteArticle(int id)
        {
            return await SendEmptyResponseBodyRequest($"{Article}/{id}", _http.DeleteAsync);
        }

        public async Task<ResponseMessage<bool>> PublishArticle(CreateUpdateArticleInput input)
        {
            return await SendEmptyResponseBodyRequest($"{Article}/publish", _http.PutAsJsonAsync, input);
        }
    }
}