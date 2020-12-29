using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider.Articles
{
    public interface IArticleProvider
    {
        Task<ResponseMessage<IEnumerable<ArticleDto>>> GetArticles();
        Task<ResponseMessage<ArticleDto>> GetArticle(int id);
        Task<ResponseMessage<bool>> CreateArticle(CreateUpdateArticleInput input);
        Task<ResponseMessage<bool>> UpdateArticle(CreateUpdateArticleInput input);
        Task<ResponseMessage<bool>> DeleteArticle(int id);
        Task<ResponseMessage<bool>> PublishArticle(CreateUpdateArticleInput input);
    }
}