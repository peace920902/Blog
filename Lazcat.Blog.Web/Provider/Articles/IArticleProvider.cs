using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider.Articles
{
    public interface IArticleProvider
    {
        Task<ResponseMessage<IEnumerable<ArticleDto>>> GetArticles(bool isGetContent = false, bool isOnlyPublished = false);
        Task<ResponseMessage<ArticleDto>> GetArticle(int id);
        Task<ResponseMessage<ArticleDto>> CreateArticle(CreateUpdateArticleInput input);
        Task<ResponseMessage<ArticleDto>> UpdateArticle(CreateUpdateArticleInput input);
        Task<ResponseMessage<bool>> DeleteArticle(int id);
        Task<ResponseMessage<bool>> PublishArticle(PublishArticleInput input);
    }
}