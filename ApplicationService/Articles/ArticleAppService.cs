using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.Domain.Articles;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Infrastructure;
using Lazcat.Blog.Infrastructure.Exceptions;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Dtos.Articles;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.Blog.ApplicationService.Articles
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
            var article = await _articleRepository.GetAll().Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
            article.Content = _articleManager.RenderMarkdown(article.Content);
            return _mapper.Map<Article, ArticleDto>(article);
        }

        public async Task<IEnumerable<ArticleDto>> GetArticleList()
        {
            return _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDto>>(await _articleRepository.GetAll().Include(x=>x.Category).ToListAsync());
        }

        public async Task UpdateArticle(int id, CreateUpdateArticleInput input)
        {
            var article = await _articleRepository.FindAsync(id);
            if (article == null) throw ExceptionBuilder.Build(HttpStatusCode.NotFound, new HttpException($"Id: {id} not match any article"));
            _mapper.Map(input, article);
            if(input.Title!=article.Title) await _articleManager.SetTitle(article,input.Title);
            article.EditTime = DateTime.Now;
            await _articleRepository.UpdateAsync(id, article);
        }

        public async Task<bool> DeleteArticle(int id)
        {
           return await _articleRepository.DeleteAsync(id);
        }
    }
}