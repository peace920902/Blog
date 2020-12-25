using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;

namespace Lazcat.Blog.ApplicationService.Articles
{
    public interface IArticleAppService
    {
        Task CreateArticle(CreateUpdateArticleInput input);
        Task<ArticleDto> GetArticle(int id);
        Task<IEnumerable<ArticleDto>> GetArticleList();
        Task UpdateArticle(int id, CreateUpdateArticleInput input);
        Task<bool> DeleteArticle(int id);
    }
}