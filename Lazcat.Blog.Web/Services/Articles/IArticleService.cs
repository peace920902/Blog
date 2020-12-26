using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.ViewModel;

namespace Lazcat.Blog.Web.Services.Articles
{
    public interface IArticleService
    {
        Task<IEnumerable<SimpleArticle>> GetArticleList();
        Task<ArticleDto> GetArticle(int id);
        Task CreateArticle(CreateUpdateArticleInput input);
        Task UpdateArticle(CreateUpdateArticleInput input);
        Task DeleteArticle(int id);
        string ConvertToHtml(string markdown);
    }
}