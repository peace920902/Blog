using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lazcat.Blog.Web.Provider.Categories;

namespace Lazcat.Blog.Web.Services.Categories
{
    public class CategoryService
    {
        private readonly ICategoryProvider _categoryProvider;

        public CategoryService(ICategoryProvider categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            var response = await _categoryProvider.GetAllCategories();
            return response.Entity.Select(x => x.Name).OrderBy(x => x);
        }

        public async Task CreateCategory(string name)
        {
            
        }
    }
}