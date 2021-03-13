using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;

namespace Lazcat.Blog.ApplicationService.Articles
{
    public interface IArticleAppService
    {
        Task<ArticleDto> CreateOrUpdateArticle(CreateUpdateArticleInput input);
        Task<ArticleDto> GetArticle(int id);
        Task<IEnumerable<ArticleDto>> GetArticleList(bool isGetContent, bool isOnlyPublished = false);
        Task<ArticleDto> UpdateArticle(int id, CreateUpdateArticleInput input);
        Task<bool> DeleteArticle(int id);
        Task PublishArticle(PublishArticleInput input);
    }
}