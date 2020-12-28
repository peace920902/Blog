using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Dtos.Categories;
using Lazcat.Blog.Models.Web;
using Lazcat.Blog.Web.Provider.Categories;

namespace Lazcat.Blog.Web.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryProvider _categoryProvider;

        public CategoryService(ICategoryProvider categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var response = await _categoryProvider.GetAllCategories();
            if (response.Entity == null)
                return new List<CategoryDto>();
            return response.Entity.OrderBy(x => x.Name);
        }

        public async Task<StandardOutput<bool>> CreateCategory(string name)
        {
            var responseMessage = await _categoryProvider.CreateCategory(new CreateUpdateCategoryInput { Name = name });
            if (responseMessage.StateCode != Setting.StateCode.OK)
            {
                return new StandardOutput<bool>
                {
                    Entity = false,
                    Message = $"Create category failed. Check if category Name: {name} is already existed."
                };
            }
            return new StandardOutput<bool>
            {
                Entity = true,
                Message = $"Create category succeed."
            };
        }

        public async Task<StandardOutput<bool>> UpdateCategory(CreateUpdateCategoryInput input)
        {
            var responseMessage = await _categoryProvider.UpdateCategory(input);
            if (responseMessage.StateCode != Setting.StateCode.OK)
            {
                return new StandardOutput<bool>
                {
                    Entity = false,
                    Message = $"Update category failed. Check if category Name: {input.Name} is already existed or id {input.Id} Not Found."
                };
            }
            return new StandardOutput<bool>
            {
                Entity = true,
                Message = $"Update category succeed."
            };
        }

        public async Task<StandardOutput<bool>> DeleteCategory(int id)
        {
            var responseMessage = await _categoryProvider.DeleteCategory(id);
            if (responseMessage.StateCode != Setting.StateCode.OK)
            {
                return new StandardOutput<bool>
                {
                    Entity = false,
                    Message = $"Delete category failed. Check if category id: {id} Not Found."
                };
            }
            return new StandardOutput<bool>
            {
                Entity = true,
                Message = $"Delete category succeed."
            };
        }
    }
}