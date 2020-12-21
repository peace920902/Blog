using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Infrastructure;
using Lazcat.Blog.Infrastructure.Exceptions;
using Lazcat.Blog.Models.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.Blog.Domain.Articles
{

    public class ArticleManager
    {
        private readonly IRepository<int, Article> _repository;

        public ArticleManager(IRepository<int, Article> repository)
        {
            _repository = repository;
        }

        public async Task<Article> CreateAsync(string title, string content, bool isPublished = false, string cover = null)
        {
            //todo
            var newArticle = new Article { Content = content, IsPublished = isPublished, Cover = cover };
            await SetTitle(newArticle, title);
            return await _repository.CreateAsync(newArticle);
        }

        public async Task<string> RenderMarkdown(string content)
        {
            return "";
        }

        public async Task SetTitle(Article article, string title)
        {
            var oldArticle = await _repository.FirstOrDefaultAsync(x => x.Title.Equals(title));
            if (oldArticle != null) throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, new HttpException { Content = "Article's title is already Existed" });
            article.Title = title;
        }
    }
}