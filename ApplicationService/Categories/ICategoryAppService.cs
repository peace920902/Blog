using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Categories;

namespace ApplicationService.Categories
{
    public interface ICategoryAppService
    {
        Task CreateCategoryAsync(CreateUpdateCategoryInput input);
        Task UpdateCategoryAsync(int id, CreateUpdateCategoryInput input);
        Task<IEnumerable<CategoryDto>> GetCategoryList();
        Task DeleteCategory(int id);
    }
}