using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.ViewModel;
using Lazcat.Blog.Web.Provider.Articles;

namespace Lazcat.Blog.Web.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleProvider _articleProvider;
        private readonly IMapper _mapper;

        public ArticleService(IArticleProvider articleProvider, IMapper mapper)
        {
            _articleProvider = articleProvider;
            _mapper = mapper;
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
        
        public async Task<string> ConvertToHtml(string markdown)
        {
            return "";
        }
    }
}