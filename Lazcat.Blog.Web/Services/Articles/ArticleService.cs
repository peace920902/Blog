using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.ViewModel;
using Lazcat.Blog.Models.Web;
using Lazcat.Blog.Web.Provider.Articles;
using Markdig;

namespace Lazcat.Blog.Web.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleProvider _articleProvider;
        private readonly IMapper _mapper;
        private readonly MarkdownPipeline _pipeline;

        public ArticleService(IArticleProvider articleProvider, IMapper mapper, MarkdownPipeline pipeline)
        {
            _articleProvider = articleProvider;
            _mapper = mapper;
            _pipeline = pipeline;
        }

        public async Task<IEnumerable<SimpleArticle>> GetArticleList()
        {
            var responseMessage = await _articleProvider.GetArticles();
            return responseMessage.StateCode != Setting.StateCode.OK ? new List<SimpleArticle>() : _mapper.Map<IEnumerable<ArticleDto>, IEnumerable<SimpleArticle>>(responseMessage.Entity);
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            var message = await _articleProvider.GetArticle(id);
            return message.Entity;
        }

        public async Task CreateArticle(CreateUpdateArticleInput input)
        {
            await _articleProvider.CreateArticle(input);
        }

        public async Task UpdateArticle(CreateUpdateArticleInput input)
        {
            await _articleProvider.UpdateArticle(input);
        }

        public async Task DeleteArticle(int id)
        {
            await _articleProvider.DeleteArticle(id);
        }

        public async Task<StandardOutput<bool>> PublishArticle(CreateUpdateArticleInput input)
        {
            var responseMessage = await _articleProvider.PublishArticle(input);
            return responseMessage.StateCode != Setting.StateCode.OK ? new StandardOutput<bool> {Entity = false, Message = $"Publish or unPublish failed. Check if id {input.Id} is not existed"} 
                : new StandardOutput<bool> {Entity = true, Message = "publish succeed"};
        }

        public string ConvertToHtml(string markdown)
        {
            return Markdown.ToHtml(markdown, _pipeline);
        }

    }
}