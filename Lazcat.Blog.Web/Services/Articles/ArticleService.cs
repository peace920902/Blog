using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntDesign;
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

        public async Task<IEnumerable<SimpleArticle>> GetArticleList(bool isGetContent = false)
        {
            var responseMessage = await _articleProvider.GetArticles(isGetContent);
            return responseMessage.StateCode != Setting.StateCode.OK ?
                new List<SimpleArticle>() : _mapper.Map<IEnumerable<ArticleDto>, IEnumerable<SimpleArticle>>(responseMessage.Entity ?? new List<ArticleDto>());
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            var message = await _articleProvider.GetArticle(id);
            return message.Entity;
        }

        public async Task<StandardOutput<ArticleDto>> CreateOrUpdateArticle(CreateUpdateArticleInput input)
        {
            var responseMessage = await _articleProvider.CreateArticle(input);
            return responseMessage.StateCode != Setting.StateCode.OK ? new StandardOutput<ArticleDto>
            {
                Entity = null,
                Message = $"CreateOrUpdateArticle failed. Reason {responseMessage.ErrorMessage}"
            } : new StandardOutput<ArticleDto> { Entity = responseMessage.Entity, Message = "CreateOrUpdateArticle succeed" };
        }

        public async Task<StandardOutput<ArticleDto>> UpdateArticle(CreateUpdateArticleInput input)
        {
            var responseMessage = await _articleProvider.UpdateArticle(input);
            return responseMessage.StateCode != Setting.StateCode.OK ? new StandardOutput<ArticleDto>
            {
                Entity = null,
                Message = $"UpdateArticle failed. Reason: {responseMessage.ErrorMessage}"
            } : new StandardOutput<ArticleDto> { Entity = responseMessage.Entity, Message = "UpdateArticle succeed" };
        }

        public async Task<StandardOutput<bool>> DeleteArticle(int id)
        {
            var responseMessage = await _articleProvider.DeleteArticle(id);
            return responseMessage.StateCode != Setting.StateCode.OK ? new StandardOutput<bool> { Entity = false, Message = $"DeleteArticle failed. Check if id {id} is existed" }
                : new StandardOutput<bool> { Entity = true, Message = "DeleteArticle succeed" };
        }

        public async Task<StandardOutput<bool>> PublishArticle(CreateUpdateArticleInput input)
        {
            var responseMessage = await _articleProvider.PublishArticle(input);
            return responseMessage.StateCode != Setting.StateCode.OK ? new StandardOutput<bool> { Entity = false, Message = $"Publish or unPublish failed. Check if id {input.Id} is existed" }
                : new StandardOutput<bool> { Entity = true, Message = "publish succeed" };
        }

        public string ConvertToHtml(string markdown)
        {
            return Markdown.ToHtml(markdown, _pipeline);
        }
    }
}