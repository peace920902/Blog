using System.Net.Http;
using System.Threading.Tasks;
using ApplicationService.Categories;
using Lazcat.Blog.Models.Dtos.Categories;
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
        [HttpPost]
        public async Task Index(CreateUpdateCategoryInput input)
        {
            await _categoryService.CreateCategoryAsync(input);
        }
    }
}