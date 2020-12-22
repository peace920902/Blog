using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.Domain.Articles;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Dtos;
using Lazcat.Blog.Models.Dtos.Articles;

namespace ApplicationService.Articles
{
    public class ArticleAppService : IArticleAppService
    {
        private readonly IRepository<int, Article> _articleRepository;
        private readonly IArticleManager _articleManager;
        private readonly IMapper _mapper;

        public ArticleAppService(IRepository<int, Article> articleRepository, IArticleManager articleManager, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _articleManager = articleManager;
            _mapper = mapper;
        }

        public async Task CreateArticle(CreateUpdateArticleInput input)
        {
            var article = await _articleManager.CreateAsync(input.Title, input.Content, input.CategoryId, input.IsPublished, input.Cover);
            await _articleRepository.CreateAsync(article);
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ArticleDto>> GetArticleList()
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateArticle(int id, CreateUpdateArticleInput input)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteArticle(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}