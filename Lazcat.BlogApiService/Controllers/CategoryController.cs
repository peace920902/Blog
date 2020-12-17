using System.Threading.Tasks;
using ApplicationService.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Lazcat.BlogApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _categoryService;

        public CategoryController(ICategoryAppService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost,Route("test")]
        public async Task Index()
        {
        }
    }
}