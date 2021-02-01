using System.Net;
using System.Threading.Tasks;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Infrastructure;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Infrastructure;
using Markdig;

namespace Lazcat.Blog.Domain.Articles
{
    public class ArticleManager : IArticleManager
    {
        private readonly MarkdownPipeline _pipeline;
        private readonly IRepository<int, Article> _repository;

        public ArticleManager(IRepository<int, Article> repository, MarkdownPipeline pipeline)
        {
            _repository = repository;
            _pipeline = pipeline;
        }

        public async Task<Article> CreateAsync(CreateUpdateArticleInput input)
        {
            //todo use auto mapper
            var article = new Article
            {
                Content = input.Content,
                CategoryId = input.CategoryId,
                IsPublished = input.IsPublished,
                Description = input.Description,
                Cover = input.Cover
            };
            await SetTitle(article, input.Title);
            return article;
        }

        public string RenderMarkdown(string content)
        {
            return Markdown.ToHtml(content, _pipeline);
        }

        public async Task SetTitle(Article article, string title)
        {
            var oldArticle = await _repository.FirstOrDefaultAsync(x => x.Title.Equals(title));
            if (oldArticle != null) throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, new HttpException { Content = "Article's title is already Existed" });
            article.Title = title;
        }
    }
}