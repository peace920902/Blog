using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lazcat.Blog.ApplicationService.Messages;
using Lazcat.Blog.Domain.Articles;
using Lazcat.Blog.Domain.Repository;
using Lazcat.Blog.Infrastructure;
using Lazcat.Blog.Models.Domain.Articles;
using Lazcat.Blog.Models.Dtos.Articles;
using Lazcat.Blog.Models.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.Blog.ApplicationService.Articles
{
    public class ArticleAppService : IArticleAppService
    {
        private readonly IArticleManager _articleManager;
        private readonly IRepository<int, Article> _articleRepository;
        private readonly IMapper _mapper;
        private readonly IMessageAppService _messageAppService;

        public ArticleAppService(IRepository<int, Article> articleRepository, IMessageAppService messageAppService, IArticleManager articleManager,
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _messageAppService = messageAppService;
            _articleManager = articleManager;
            _mapper = mapper;
        }

        public async Task<ArticleDto> CreateOrUpdateArticle(CreateUpdateArticleInput input)
        {
            if (input.CategoryId <= 0)
                throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, new HttpException("category not exist"));
            if (input.Id > 0) return await UpdateArticle(input.Id, input);

            var article = await _articleManager.CreateAsync(input);
            var createdArticle = await _articleRepository.CreateAsync(article);
            return _mapper.Map<Article, ArticleDto>(createdArticle);
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            var article = await _articleRepository.GetAll().Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
            if (article == null) throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, new HttpException("Article not exist"));
            var returnArticle = new ArticleDto();
            _mapper.Map(article, returnArticle);
            returnArticle.Messages = await _messageAppService.GetMessages(id);
            return returnArticle;
        }

        public async Task<IEnumerable<ArticleDto>> GetArticleList(bool isGetContent, bool isOnlyPublished = false)
        {
            var articles = isOnlyPublished
                ? _articleRepository.GetAll().Include(x => x.Category).Where(x => x.IsPublished)
                : _articleRepository.GetAll().Include(x => x.Category);
            var articleList = isGetContent
                ? await articles.ToListAsync()
                : await articles
                    .Select(x => new Article
                    {
                        Id = x.Id,
                        Title = x.Title,
                        EditTime = x.EditTime,
                        IsPublished = x.IsPublished,
                        PublishTime = x.PublishTime,
                        Description = x.Description,
                        Cover = x.Cover,
                        Category = x.Category
                    }).ToListAsync();
            articleList ??= new List<Article>();

            return _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDto>>(articleList);
        }

        public async Task<ArticleDto> UpdateArticle(int id, CreateUpdateArticleInput input)
        {
            if (input.CategoryId <= 0)
                throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, new HttpException("category not exist"));
            var article = await _articleRepository.FindAsync(id);
            if (article == null) throw ExceptionBuilder.Build(HttpStatusCode.NotFound, new HttpException($"Id: {id} not match any article"));
            _mapper.Map(input, article);
            if (input.Title != article.Title) await _articleManager.SetTitle(article, input.Title);
            article.EditTime = DateTime.Now;
            var updateArticle = await _articleRepository.UpdateAsync(article.Id, article);
            return _mapper.Map<Article, ArticleDto>(updateArticle);
        }

        public async Task PublishArticle(PublishArticleInput input)
        {
            var article = await _articleRepository.FindAsync(input.Id);
            if (article == null) throw ExceptionBuilder.Build(HttpStatusCode.NotFound, new HttpException($"Id: {input.Id} not match any article"));
            if (article.IsPublished == input.IsPublished) return;

            article.IsPublished = input.IsPublished;
            article.PublishTime = input.IsPublished ? DateTime.Now : null;

            await _articleRepository.UpdateAsync(article.Id, article);
        }

        public async Task<bool> DeleteArticle(int id)
        {
            return await _articleRepository.DeleteAsync(id);
        }
    }
}