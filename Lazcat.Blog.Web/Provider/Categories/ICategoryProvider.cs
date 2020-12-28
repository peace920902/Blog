using System.Collections.Generic;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Categories;
using Lazcat.Blog.Models.Web;

namespace Lazcat.Blog.Web.Provider.Categories
{
    public interface ICategoryProvider
    {
        Task<ResponseMessage<IEnumerable<CategoryDto>>> GetAllCategories();
        Task<ResponseMessage<bool>> CreateCategory(CreateUpdateCategoryInput input);
        Task<ResponseMessage<bool>> UpdateCategory(CreateUpdateCategoryInput input);
        Task<ResponseMessage<bool>> DeleteCategory(int id);
    }
}