using System.Threading.Tasks;
using Lazcat.Blog.Models.Domain.Articles;

namespace Lazcat.Blog.Domain.Articles
{
    public interface IArticleManager
    {
        Task<Article> CreateAsync(string title ,string content, int categoryId, bool isPublished = false, string cover = null);
        Task<string> RenderMarkdown(string content);
        Task SetTitle(Article article, string title);
    }
}