using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.ViewModel;
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
            var articles = await _articleProvider.GetArticles();
            return _mapper.Map<IEnumerable<ArticleDto>, IEnumerable<SimpleArticle>>(articles); 
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            return await _articleProvider.GetArticle(id);
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
        
        public string ConvertToHtml(string markdown)
        {
            return Markdown.ToHtml(markdown, _pipeline);
        }
        
    }
}