using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Categories;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<StandardOutput<bool>> CreateCategory(string name);
        Task<StandardOutput<bool>> UpdateCategory(CreateUpdateCategoryInput input);
        Task<StandardOutput<bool>> DeleteCategory(int id);
    }
}