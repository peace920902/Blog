using System.Threading.Tasks;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Dtos.Articles;

namespace Lazcat.Blog.Domain.Articles
{
    public interface IArticleManager
    {
        Task<Article> CreateAsync(CreateUpdateArticleInput input);
        string RenderMarkdown(string content);
        Task SetTitle(Article article, string title);
    }
}