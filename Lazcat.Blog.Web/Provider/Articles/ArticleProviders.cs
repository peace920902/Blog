using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider.Articles
{
    public class ArticleProviders : ProviderBase, IArticleProvider
    {
        private readonly HttpClient _http;

        public ArticleProviders(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient(Define.DefaultHttpClient);
        }

        public async Task<ResponseMessage<IEnumerable<ArticleDto>>> GetArticles(bool isGetContent = false, bool isOnlyPublished = false)
        {
            return isOnlyPublished
                ? await SendRequest(
                    $"{Define.ProviderRoutes.ArticleRoute.Article}/{Define.ProviderRoutes.ArticleRoute.GetOnlyPublished}?{Define.ProviderRoutes.ArticleRoute.QueryString.IsGetContent}={isGetContent}",
                    _http.GetFromJsonAsync<IEnumerable<ArticleDto>>)
                : await SendRequest(
                    $"{Define.ProviderRoutes.ArticleRoute.Article}/{Define.ProviderRoutes.ArticleRoute.GetAll}?{Define.ProviderRoutes.ArticleRoute.QueryString.IsGetContent}={isGetContent}",
                    _http.GetFromJsonAsync<IEnumerable<ArticleDto>>);
        }

        public async Task<ResponseMessage<ArticleDto>> GetArticle(int id)
        {
            return await SendRequest($"{Define.ProviderRoutes.ArticleRoute.Article}/{id}", _http.GetFromJsonAsync<ArticleDto>);
        }

        public async Task<ResponseMessage<ArticleDto>> CreateArticle(CreateUpdateArticleInput input)
        {
            return await SendNeedDeserializedRequest<ArticleDto, CreateUpdateArticleInput>(Define.ProviderRoutes.ArticleRoute.Article, _http.PostAsJsonAsync,
                input);
        }

        public async Task<ResponseMessage<ArticleDto>> UpdateArticle(CreateUpdateArticleInput input)
        {
            return await SendNeedDeserializedRequest<ArticleDto, CreateUpdateArticleInput>(
                $"{Define.ProviderRoutes.ArticleRoute.Article}/{Define.ProviderRoutes.ArticleRoute.UpdateContent}", _http.PutAsJsonAsync, input);
        }

        public async Task<ResponseMessage<bool>> DeleteArticle(int id)
        {
            return await SendEmptyResponseBodyRequest($"{Define.ProviderRoutes.ArticleRoute.Article}/{id}", _http.DeleteAsync);
        }

        public async Task<ResponseMessage<bool>> PublishArticle(PublishArticleInput input)
        {
            return await SendEmptyResponseBodyRequest($"{Define.ProviderRoutes.ArticleRoute.Article}/{Define.ProviderRoutes.ArticleRoute.Publish}",
                _http.PutAsJsonAsync, input);
        }
    }
}