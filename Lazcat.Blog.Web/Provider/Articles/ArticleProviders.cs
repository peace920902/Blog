using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;

namespace Lazcat.Blog.Web.Provider.Articles
{
    public class ArticleProviders: IArticleProvider
    {
        private readonly HttpClient _http;

        public ArticleProviders(HttpClient http)
        {
            _http = http;
        }
        public async Task<IEnumerable<ArticleDto>> GetArticles()
        {
            var articles = await _http.GetFromJsonAsync<IEnumerable<ArticleDto>>("Article/all");
            return articles;
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            var article = await _http.GetFromJsonAsync<ArticleDto>($"Article/{id}");
            return article;
        }

        public async Task CreateArticle(CreateUpdateArticleInput input)
        {
            await _http.PostAsJsonAsync("Article", input);
        }

        public async Task UpdateArticle(CreateUpdateArticleInput input)
        {
            await _http.PutAsJsonAsync("Article", input);
        }

        public async Task DeleteArticle(int id)
        {
            await _http.DeleteAsync($"Article/{id}");
        }
    }
}