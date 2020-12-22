using System.Threading.Tasks;
using ApplicationService.Articles;
using Lazcat.Blog.Models.Dtos;
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
        public async Task CreateArticle(CreateUpdateArticleInput input)
        {
            await _articleAppService.CreateArticle(input);
        }
    }
}