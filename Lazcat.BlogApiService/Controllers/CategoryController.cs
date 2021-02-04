using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Lazcat.Blog.ApplicationService.Categories;
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
        public async Task Create(CreateUpdateCategoryInput input)
        {
            await _categoryService.CreateCategoryAsync(input);
        }

        [HttpDelete, Route("{id}")]

        public async Task Delete(int id)
        {
            await _categoryService.DeleteCategory(id);
        }

        [HttpPut]
        public async Task Update(CreateUpdateCategoryInput input)
        {
            await _categoryService.UpdateCategoryAsync(input.Id, input);
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetList()
        {
            return await _categoryService.GetCategoryList();
        }
    }
}