using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Categories;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider.Categories
{
    public class CategoryProvider :ProviderBase, ICategoryProvider
    {
        private readonly HttpClient _http;
        public const string Category = "category";
        public CategoryProvider(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient(Setting.DefaultHttpClient);
        }

        public async Task<ResponseMessage<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            return await SendRequest(Category, _http.GetFromJsonAsync<IEnumerable<CategoryDto>>);
        }

        public async Task<ResponseMessage<bool>> CreateCategory(CreateUpdateCategoryInput input)
        {
            return await SendEmptyResponseBodyRequest(Category, _http.PostAsJsonAsync, input);
        }

        public async Task<ResponseMessage<bool>> UpdateCategory(CreateUpdateCategoryInput input)
        {
            return await SendEmptyResponseBodyRequest(Category, _http.PutAsJsonAsync, input);
        }

        public async Task<ResponseMessage<bool>> DeleteCategory(int id)
        {
            return await SendEmptyResponseBodyRequest(Category + $"/{id}", _http.DeleteAsync);
        }
    }
}