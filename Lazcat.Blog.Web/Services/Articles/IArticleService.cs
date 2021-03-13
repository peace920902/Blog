using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.ViewModel;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Services.Articles
{
    public interface IArticleService
    {
        Task<IEnumerable<SimpleArticle>> GetArticleList(bool isGetContent = false, bool isOnlyPublished = false);
        Task<ArticleDto> GetArticle(int id);
        Task<StandardOutput<ArticleDto>> CreateOrUpdateArticle(CreateUpdateArticleInput input);
        Task<StandardOutput<ArticleDto>> UpdateArticle(CreateUpdateArticleInput input);
        Task<StandardOutput<bool>> DeleteArticle(int id);
        string ConvertToHtml(string markdown);
        Task<StandardOutput<bool>> PublishArticle(PublishArticleInput input);
    }
}