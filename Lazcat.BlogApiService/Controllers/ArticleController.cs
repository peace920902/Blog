using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.ApplicationService.Articles;
using Lazcat.Blog.Models.Dtos.Articles;
using Microsoft.AspNetCore.Mvc;

namespace Lazcat.BlogApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleAppService _articleAppService;

        public ArticleController(IArticleAppService articleAppService)
        {
            _articleAppService = articleAppService;
        }

        [HttpPost]
        public async Task<ArticleDto> CreateOrUpdateArticle(CreateUpdateArticleInput input)
        {
            return await _articleAppService.CreateOrUpdateArticle(input);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<ArticleDto>> GetAll(bool isGetContent)
        {
            return await _articleAppService.GetArticleList(isGetContent);
        }

        [HttpGet]
        [Route("AllPublished")]
        public async Task<IEnumerable<ArticleDto>> GetAllPublished(bool isGetContent)
        {
            return await _articleAppService.GetArticleList(isGetContent, true);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ArticleDto> Get(int id)
        {
            return await _articleAppService.GetArticle(id);
        }

        [HttpPut]
        [Route("content")]
        public async Task<ArticleDto> UpdateContent(CreateUpdateArticleInput input)
        {
            return await _articleAppService.UpdateArticle(input.Id, input);
        }

        [HttpPut]
        [Route("publish")]
        public async Task PublishArticle(PublishArticleInput input)
        {
            await _articleAppService.PublishArticle(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _articleAppService.DeleteArticle(id);
        }
    }
}